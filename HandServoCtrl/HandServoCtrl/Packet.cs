using System.Drawing.Drawing2D;
using System.Text;
using String = System.String;

namespace HandServoCtrl
{
    /// <summary>
    /// Commands
    /// </summary>
    internal enum Commands
    {
        GET_VERSION = (byte)0x00,
        SET_SERVO = (byte)0x01,
        GET_SERVO = (byte)0x02,
        SET_ALL_SERVO = (byte)0x03,
        GET_ALL_SERVO = (byte)0x04,
        SET_SERVO_BOUNDS = (byte)0x05,
        GET_SERVO_BOUNDS = (byte)0x06
    }

    /// <summary>
    /// Packet
    /// </summary>
    internal class Packet
    {
        static byte _counter = 0;
        public byte[] _data;
        /// <summary>
        /// 
        /// </summary>
        private Packet() { _data = new byte[4]; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public Packet(byte[] data)
        {
            _data = new byte[data.Length];
            Array.Copy(data, _data, data.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="payload"></param>
        public Packet(byte cmd, byte[]? payload = null)
        {
            if (payload != null)
            {
                _data = new byte[payload.Length + 4];
                Array.Copy(payload, 0, _data, 4, payload.Length);
                _data[2] = (byte)payload.Length;
            }
            else
            {
                _data = new byte[4];
                _data[2] = 0;
            }
            _data[0] = 0xFE;
            _data[1] = cmd;
            _data[3] = _counter++;
        }

        public byte[] Data
        {
            get { return _data; }
        }

        public byte[] Header
        {
            get { return _data[..3]; }
        }

        public byte[] Payload
        {
            get { return _data[4..]; }
        }
        public byte CMD
        {
            get { return _data[1]; }
        }

        public byte Size
        {
            get { return _data[2]; }
        }

        public byte Counter
        {
            get { return _data[3]; }
        }

        public static List<Packet>? Parse(byte[] received_data, int bytes_cnt, ref uint position)
        {
            List<Packet>? packets = null;
            uint i = 0;
            while (i < bytes_cnt)
            {
                if (received_data[i] != 0xFE)
                {
                    i++;
                    continue;
                }

                if ((i + 4) > bytes_cnt)
                {
                    //Console.WriteLine(String.Format("Packet not full [{0:X}]", received_data[i]));
                    i++;
                    continue;
                }

                byte sz = received_data[i + 2];

                if (i + 4 + sz > bytes_cnt)
                {
                    //Console.WriteLine(String.Format("Packet full [{0:X}]", received_data[i]));
                    i++;
                    continue;
                }

                byte[] data = new byte[4 + sz];
                Array.Copy(received_data, i, data, 0, 4 + sz);
                Packet pkt = new Packet(data);
                Console.WriteLine("<<<" + ToHexString(pkt.Data));
                if (packets == null)
                {
                    packets = new List<Packet>() { pkt };
                }
                else
                {
                    packets.Add(pkt);
                }


                i += (uint)sz + 4;
            };
            position = i;
            return packets;
        }

        private static string ToHexString(byte[] bytes)
        {
            var sb = new StringBuilder();

            foreach (var b in bytes)
            {
                sb.Append(string.Format(" {0:X2}",b));
            }

            return sb.ToString();
        }

        public static void Send(System.IO.Ports.SerialPort? port, byte[] data)
        {
            Console.WriteLine(">>>" + ToHexString(data));
            port?.Write(data, 0, data.Length);
        }

    }
}
