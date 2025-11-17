using System;
using System.IO.Ports;
using System.Collections.Generic;

namespace HandServoCtrl
{
    internal class ServoController
    {
        private SerialPort _serialPort;
        private Dictionary<int, int> _servoStates = new();

        public ServoController(string portName)
        {
            _serialPort = new SerialPort(portName, 9600);
            _serialPort.DataReceived += OnDataReceived;
            _serialPort.Open();

            for (int i = 1; i <= 5; i++)
                _servoStates[i] = 90;
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string input = _serialPort.ReadLine().Trim();
            string response = HandleCommand(input);
            _serialPort.WriteLine(response);
        }

        private string HandleCommand(string input)
        {
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return "ERR Empty";

            switch (parts[0].ToUpper())
            {
                case "SET":
                    if (parts.Length != 3) return "ERR Syntax";
                    if (!int.TryParse(parts[1], out int id) || id < 1 || id > 5)
                        return "ERR ID";
                    if (!int.TryParse(parts[2], out int angle) || angle < 0 || angle > 180)
                        return "ERR ANGLE";

                    _servoStates[id] = angle;

                    return $"OK SET {id} {angle}";

                case "GET":
                    if (parts.Length != 2) return "ERR Syntax";
                    if (!int.TryParse(parts[1], out id) || !_servoStates.ContainsKey(id))
                        return "ERR ID";

                    return $"STATE {id} {_servoStates[id]}";

                default:
                    return "ERR UnknownCmd";
            }
        }
    }
}
