#include <string.h>
#include <stdio.h>

#include "freertos/FreeRTOS.h"
#include "freertos/task.h"
#include "esp_log.h"
#include "driver/mcpwm_prelude.h"
#include "driver/uart.h"
#include "driver/gpio.h"

#define UART_NUM UART_NUM_0
#define TXD_PIN GPIO_NUM_1
#define RXD_PIN GPIO_NUM_3
#define BUF_SIZE 1024

#define SERVO_MIN_PULSEWIDTH_US 500
#define SERVO_MAX_PULSEWIDTH_US 2500
#define SERVO_MIN_DEGREE -90
#define SERVO_MAX_DEGREE 90

#define SERVO_TIMEBASE_RESOLUTION_HZ 1000000
#define SERVO_TIMEBASE_PERIOD 20000

#define SERVOS_CNT 5
static const char* Version = "v1.0";
mcpwm_timer_handle_t _timer[SERVOS_CNT];
mcpwm_oper_handle_t _oper[SERVOS_CNT];
mcpwm_cmpr_handle_t _comparator[SERVOS_CNT];
mcpwm_gen_handle_t _generator[SERVOS_CNT];
int _angle[SERVOS_CNT];

#define UART_RX_BUF_SIZE 256
uint8_t uart_rx_buf[UART_RX_BUF_SIZE];
int uart_rx_len = 0;

enum Commands
{
    GET_VERSION = (uint8_t)0x00,
    SET_SERVO = (uint8_t)0x01,
    GET_SERVO = (uint8_t)0x02,
    SET_ALL_SERVO = (uint8_t)0x03,
    GET_ALL_SERVO = (uint8_t)0x04,
    SET_SERVO_BOUNDS = (uint8_t)0x05,
    GET_SERVO_BOUNDS = (uint8_t)0x06
};

static inline uint32_t angle_to_compare(int angle) {
    return (angle - SERVO_MIN_DEGREE) * (SERVO_MAX_PULSEWIDTH_US - SERVO_MIN_PULSEWIDTH_US) /
        (SERVO_MAX_DEGREE - SERVO_MIN_DEGREE) + SERVO_MIN_PULSEWIDTH_US;
}

void servo_drv_create(int id, int group, int gpio) {
    mcpwm_timer_config_t timer_cfg = {
        .group_id = group,
        .clk_src = MCPWM_TIMER_CLK_SRC_DEFAULT,
        .resolution_hz = SERVO_TIMEBASE_RESOLUTION_HZ,
        .period_ticks = SERVO_TIMEBASE_PERIOD,
        .count_mode = MCPWM_TIMER_COUNT_MODE_UP
    };
    mcpwm_new_timer(&timer_cfg, &_timer[id]);

    mcpwm_operator_config_t oper_cfg = { .group_id = group };
    mcpwm_new_operator(&oper_cfg, &_oper[id]);
    mcpwm_operator_connect_timer(_oper[id], _timer[id]);

    mcpwm_comparator_config_t cmp_cfg = { .flags.update_cmp_on_tez = true };
    mcpwm_new_comparator(_oper[id], &cmp_cfg, &_comparator[id]);

    mcpwm_generator_config_t gen_cfg = { .gen_gpio_num = gpio };
    mcpwm_new_generator(_oper[id], &gen_cfg, &_generator[id]);

    mcpwm_comparator_set_compare_value(_comparator[id], angle_to_compare(0));
}

void servo_drv_start(int id) {
    mcpwm_generator_set_action_on_timer_event(_generator[id],
        MCPWM_GEN_TIMER_EVENT_ACTION(MCPWM_TIMER_DIRECTION_UP, MCPWM_TIMER_EVENT_EMPTY, MCPWM_GEN_ACTION_HIGH));
    mcpwm_generator_set_action_on_compare_event(_generator[id],
        MCPWM_GEN_COMPARE_EVENT_ACTION(MCPWM_TIMER_DIRECTION_UP, _comparator[id], MCPWM_GEN_ACTION_LOW));

    mcpwm_timer_enable(_timer[id]);
    mcpwm_timer_start_stop(_timer[id], MCPWM_TIMER_START_NO_STOP);
}

void servo_drv_update(int id) {
    mcpwm_comparator_set_compare_value(_comparator[id], angle_to_compare(_angle[id]));
}

void uart_init() {
    const uart_config_t uart_config = {
        .baud_rate = 115200,
        .data_bits = UART_DATA_8_BITS,
        .parity = UART_PARITY_DISABLE,
        .stop_bits = UART_STOP_BITS_1,
        .flow_ctrl = UART_HW_FLOWCTRL_DISABLE
    };
    uart_driver_install(UART_NUM, BUF_SIZE * 2, 0, 0, NULL, 0);
    uart_param_config(UART_NUM, &uart_config);
    uart_set_pin(UART_NUM, TXD_PIN, RXD_PIN, UART_PIN_NO_CHANGE, UART_PIN_NO_CHANGE);
}

void handle_uart_command(uint8_t cmd, uint8_t* payload, uint8_t sz) {
    switch (cmd) {
        case GET_VERSION:
        {
            sz = strlen(Version);
            uint8_t data[sz + 4];
            memcpy(data + 4, Version, sz);
            data[0] = 0xFE;
            data[1] = GET_VERSION;
            data[2] = sz;

            uart_write_bytes(UART_NUM, data, sz + 4);
        }
        break;
        case SET_SERVO:
        {
            if (sz >= 2) {
                uint8_t servo = payload[0] - 1;
                int angle = payload[1] - 90;
                if (servo < SERVOS_CNT) {
                    _angle[servo] = angle;
                    servo_drv_update(servo);
                    uint8_t data[6];
                    data[0] = 0xFE;
                    data[1] = SET_SERVO;
                    data[2] = 2;
                    data[4] = payload[0];
                    data[5] = payload[1];
                    uart_write_bytes(UART_NUM, data, 6);
                }
            }

        }break;
        case GET_SERVO:
        {
            if (sz >= 2) {
                uint8_t servo = payload[0] - 1;
                if (servo < SERVOS_CNT) {
                    uint8_t data[6];
                    data[0] = 0xFE;
                    data[1] = GET_SERVO;
                    data[2] = 2;
                    data[4] = payload[0];
                    data[5] = (uint8_t)(_angle[servo] + 90);
                    uart_write_bytes(UART_NUM, data, 6);
                }
            }

        }break;
    }
}

void parse_uart_buffer() {
    while (uart_rx_len >= 4) {
        int start = -1;
        for (int i = 0; i < uart_rx_len - 3; ++i) {
            if (uart_rx_buf[i] == 0xFE) {
                start = i;
                break;
            }
        }

        if (start == -1) {
            uart_rx_len = 0;
            return;
        }

        if (start > 0) {
            memmove(uart_rx_buf, uart_rx_buf + start, uart_rx_len - start);
            uart_rx_len -= start;
        }

        uint8_t cmd = uart_rx_buf[1];
        uint8_t sz = uart_rx_buf[2];

        if (uart_rx_len < sz + 4) {
            return;
        }

        handle_uart_command(cmd, &uart_rx_buf[4], sz);

        int packet_len = sz + 4;
        uart_rx_len -= packet_len;
        memmove(uart_rx_buf, uart_rx_buf + packet_len, uart_rx_len);
    }
}

void uart_send(const char* msg) {
    uart_write_bytes(UART_NUM, msg, strlen(msg));
}

void uart_receive_task(void* arg) {
    uint8_t temp[64];
    while (true) {
        int len = uart_read_bytes(UART_NUM, temp, sizeof(temp), 20 / portTICK_PERIOD_MS);
        if (len > 0) {
            for (int i = 0; i < len && uart_rx_len < UART_RX_BUF_SIZE; ++i) {
                uart_rx_buf[uart_rx_len++] = temp[i];
            }

            parse_uart_buffer();
        }
    }
}

void app_main(void) {
    uart_init();
    xTaskCreate(uart_receive_task, "uart_rx_task", 2048, NULL, 10, NULL);

    servo_drv_create(0, 0, 15);
    servo_drv_create(1, 0, 2);
    servo_drv_create(2, 0, 0);
    servo_drv_create(3, 1, 4);
    servo_drv_create(4, 1, 16);

    for (int id = 0; id < SERVOS_CNT; ++id) {
        servo_drv_start(id);
        _angle[id] = 0;
        servo_drv_update(id);
    }

}
