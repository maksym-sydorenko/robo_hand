using System.IO.Ports;
using System.Text;
using System.Runtime.InteropServices;

#nullable enable
namespace HandServoCtrl
{
    public partial class MainForm : Form
    {
        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();

        AboutBox _aboutBox = new AboutBox();
        SerialPort? _port = null;
        UartForm? _uartForm = null;
        bool _isConnected = false;
        bool _isUI_Enabled = false;
        private byte[] _buffer = new byte[0x8000];
        private int _offset = 0;
        private Queue<Packet> _packets = new Queue<Packet>();

        private bool UI_Enabled
        {
            set
            {
                _isUI_Enabled = value;
                tbServo1.Enabled = _isUI_Enabled;
                tbServo2.Enabled = _isUI_Enabled;
                tbServo3.Enabled = _isUI_Enabled;
                tbServo4.Enabled = _isUI_Enabled;
                tbServo5.Enabled = _isUI_Enabled;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            UI_Enabled = false;
            _uartForm = new UartForm(ref _port);
            if (_port != null)
            {
                _port.DataReceived += _port_DataReceived;
            }
            AllocConsole();

            Task.Run(() => HandlePacketTask());
        }

        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            List<Packet>? packets = null;
            int bytes_cnt = ((SerialPort)sender).BytesToRead;
            if (bytes_cnt > 0)
            {
                uint position = 0;

                lock (_buffer)
                {
                    bytes_cnt = Math.Min(bytes_cnt, _buffer.Length - _offset);
                    ((SerialPort)sender).Read(_buffer, _offset, bytes_cnt);
                }

                packets = Packet.Parse(_buffer, _offset + bytes_cnt, ref position);

                if (_offset + bytes_cnt > position)
                {
                    _offset = (int)(_offset + bytes_cnt - position);
                    Array.Copy(_buffer, position, _buffer, 0, _offset);
                }
                else if (_offset + bytes_cnt < position)
                {
                    _offset = 0;
                }
                else
                {
                    _offset = 0;
                }
                //Console.WriteLine(String.Format(",out position {0}, out offset {1}", position, _offset));
                if (packets == null || packets.Count == 0)
                {
                    return;
                }

                lock (_packets)
                {
                    foreach (var pckt in packets)
                    {
                        _packets.Enqueue(pckt);
                    }
                }
            }
        }

        #region UI events
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _aboutBox.ShowDialog();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_isConnected)
                {
                    if (_port != null)
                    {
                        _port.Open();
                        connectToolStripMenuItem.Text = "Disconnect";
                        this.Text = string.Format("Handle servo control [Connected {0}]", _port.PortName);
                        _isConnected = true;
                        UI_Enabled = true;
                        Packet pkt = new Packet((byte)Commands.GET_VERSION);
                        Packet.Send(_port, pkt.Data);
                    }

                }
                else
                {
                    _port?.Close();
                    connectToolStripMenuItem.Text = "Connect";
                    _isConnected = false;
                    UI_Enabled = false;
                    this.Text = string.Format("Handle servo control [Disconnected]");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_isConnected)
            {
                _uartForm = new UartForm(ref _port);
                _uartForm?.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please disconnect before configure");
            }
        }

        private void tbServo1_Scroll(object sender, EventArgs e)
        {
            try
            {
                byte[] payload = new byte[] { 0x01, (byte)tbServo1.Value };
                Packet pkt = new Packet((byte)Commands.SET_SERVO, payload);
                tbServo1.Enabled = false;
                Packet.Send(_port, pkt.Data);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbServo2_Scroll(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    byte[] payload = new byte[] { 0x02, (byte)tbServo2.Value };
                    Packet pkt = new Packet((byte)Commands.SET_SERVO, payload);
                    tbServo2.Enabled = false;
                    Packet.Send(_port, pkt.Data);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbServo3_Scroll(object sender, EventArgs e)
        {
            try
            {
                byte[] payload = new byte[] { 0x03, (byte)tbServo3.Value };
                Packet pkt = new Packet((byte)Commands.SET_SERVO, payload);
                tbServo3.Enabled = false;
                Packet.Send(_port, pkt.Data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbServo4_Scroll(object sender, EventArgs e)
        {
            try
            {
                byte[] payload = new byte[] { 0x04, (byte)tbServo4.Value };
                Packet pkt = new Packet((byte)Commands.SET_SERVO, payload);
                tbServo4.Enabled = false;
                Packet.Send(_port, pkt.Data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbServo5_Scroll(object sender, EventArgs e)
        {
            try
            {
                byte[] payload = new byte[] { 0x05, (byte)tbServo5.Value };
                Packet pkt = new Packet((byte)Commands.SET_SERVO, payload);
                tbServo5.Enabled = false;
                Packet.Send(_port, pkt.Data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region TASKS

        private string ToHexString(byte[] bytes)
        {
            var sb = new StringBuilder();

            foreach (var b in bytes)
            {
                sb.Append(b.ToString(" X2"));
            }

            return sb.ToString();
        }

        private void HandlePacketTask()
        {
            try
            {
                while (true)
                {
                    if (_packets.Count == 0)
                    {
                        continue;
                    }

                    Packet pkt = _packets.Dequeue();
                    switch ((Commands)pkt.CMD)
                    {
                        case Commands.GET_VERSION:
                            {
                                string version = string.Format("FW version: {0}", Encoding.ASCII.GetString(pkt.Payload));
                                if (this.InvokeRequired)
                                {
                                    this.Invoke((MethodInvoker)(() => { tssStatus.Text = version; }));
                                }
                                else
                                {
                                    tssStatus.Text = version;
                                }
                            }
                            break;
                        case Commands.SET_SERVO:
                            {
                                if (this.InvokeRequired)
                                {
                                    this.Invoke((MethodInvoker)(() =>
                                    {
                                        if (pkt.Payload[0] == 1) { tbServo1.Enabled = true; }
                                        if (pkt.Payload[0] == 2) { tbServo2.Enabled = true; }
                                        if (pkt.Payload[0] == 3) { tbServo3.Enabled = true; }
                                        if (pkt.Payload[0] == 4) { tbServo4.Enabled = true; }
                                        if (pkt.Payload[0] == 5) { tbServo5.Enabled = true; }
                                    }));
                                }
                                else
                                {
                                    if (pkt.Payload[0] == 1) { tbServo1.Enabled = true; }
                                    if (pkt.Payload[0] == 2) { tbServo2.Enabled = true; }
                                    if (pkt.Payload[0] == 3) { tbServo3.Enabled = true; }
                                    if (pkt.Payload[0] == 4) { tbServo4.Enabled = true; }
                                    if (pkt.Payload[0] == 5) { tbServo5.Enabled = true; }
                                }
                            }
                            break;
                        case Commands.GET_SERVO:
                            {

                            }
                            break;
                        case Commands.SET_ALL_SERVO:
                            {
                                string result = string.Format("SET_ALL_SERVO: {0}", ToHexString(pkt.Payload));
                                Console.WriteLine(result);
                            }
                            break;
                        case Commands.GET_ALL_SERVO:
                            {
                                string result = string.Format("GET_ALL_SERVO: {0}", ToHexString(pkt.Payload));
                                Console.WriteLine(result);
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
        #endregion
    }
}
