using Microsoft.Win32;
using System.IO.Ports;

#nullable enable
namespace HandServoCtrl
{
    public partial class UartForm : Form
    {
        private string key_path = @"SOFTWARE\MaxSyd\HandleCntrl";
        public UartForm(ref SerialPort? _port)
        {
            InitializeComponent();

            cbPortName.Items.Clear();
            string[] pns = SerialPort.GetPortNames();
            foreach (string s in pns)
            {
                if (cbPortName.FindStringExact(s) == -1)
                    cbPortName.Items.Add(s);
            }

            cbBaudRate.Items.Clear();
            cbBaudRate.Items.Add("9600");
            cbBaudRate.Items.Add("57600");
            cbBaudRate.Items.Add("115200");
            cbBaudRate.SelectedIndex = 2;

            cbParity.Items.Clear();
            cbParity.Items.Add(System.IO.Ports.Parity.Even.ToString());
            cbParity.Items.Add(System.IO.Ports.Parity.Odd.ToString());
            cbParity.Items.Add(System.IO.Ports.Parity.None.ToString());
            cbParity.SelectedIndex = 2;

            cbDataBits.Items.Clear();
            cbDataBits.Items.Add("8");
            cbDataBits.SelectedIndex = 0;

            cbStopBits.Items.Clear();
            cbStopBits.Items.Add(System.IO.Ports.StopBits.None.ToString());
            cbStopBits.Items.Add(System.IO.Ports.StopBits.One.ToString());
            cbStopBits.Items.Add(System.IO.Ports.StopBits.OnePointFive.ToString());

            cbStopBits.SelectedIndex = 1;

            UpdateSerialPorts();
            if (PortName != "")
            {
                if (_port == null)
                {
                    _port = new SerialPort(PortName, BaudRate, Parity, DataBits, StopBits);
                }
                else
                {
                    _port.PortName = PortName;
                }
            }

        }

        private void UpdateSerialPorts()
        {
            cbPortName.Items.Clear();
            foreach (string s in SerialPort.GetPortNames())
            {
                if (cbPortName.FindStringExact(s) == -1)
                    cbPortName.Items.Add(s);
            }
            RegistryKey? key = Registry.CurrentUser?.OpenSubKey(key_path);
            string? serial = "COM1";
            if (key != null)
            {
                serial = key.GetValue("Serial")?.ToString();
                if (cbPortName.FindStringExact(serial) != -1)
                {
                    cbPortName.SelectedItem = serial;
                }
            }
            else
            {
                key = Registry.CurrentUser?.CreateSubKey(key_path);
                key?.SetValue("Serial", serial);
            }
            cbPortName.SelectedItem = serial;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(key_path);
            key.SetValue("Serial", PortName);

            this.Close();
            DialogResult = DialogResult.OK;
        }

        private void btCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }

        #region port

        /// <summary>
        /// Port Name
        /// </summary>
        public string PortName
        {
            get
            {
                string? str = cbPortName.SelectedItem?.ToString();
                if (str != null)
                    return str;
                else
                    return "";
            }
        }
        private int BaudRate
        {
            get
            {
                string? str = cbBaudRate.SelectedItem?.ToString();
                if (str == null)
                {
                    return 115200;
                }
                else
                {
                    return int.Parse(str);
                }
            }
        }

        private Parity Parity
        {
            get
            {
                System.IO.Ports.Parity res = Parity.None;
                switch (cbStopBits.SelectedItem?.ToString())
                {
                    case "Even": { res = System.IO.Ports.Parity.Even; } break;
                    case "Odd": { res = System.IO.Ports.Parity.Odd; } break;
                    case "None": { res = System.IO.Ports.Parity.None; } break;
                    case null: { res = System.IO.Ports.Parity.None; } break;
                }
                return res;
            }
        }

        private int DataBits { get { return 8; } }

        private StopBits StopBits
        {
            get
            {
                System.IO.Ports.StopBits res = StopBits.None;
                switch (cbStopBits.SelectedItem?.ToString())
                {
                    case "None": { res = System.IO.Ports.StopBits.None; } break;
                    case "One": { res = System.IO.Ports.StopBits.One; } break;
                    case "OnePointFive": { res = System.IO.Ports.StopBits.OnePointFive; } break;
                    case null: { res = System.IO.Ports.StopBits.One; } break;
                }
                return res;
            }
        }

        #endregion
    }
}
