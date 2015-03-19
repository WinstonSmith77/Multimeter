using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AccessDevice
    {
        public AccessDevice()
        {
            var ports = SerialPort.GetPortNames();

            if (ports.Count() != 1)
            {
                throw new ArgumentException("Wrong number of ports!");
            }

            _port = InitComPort(ports.First());
            _port.Open();
        }

        private SerialPort InitComPort(string name)
        {
            SerialPort port = new SerialPort
            {
                BaudRate = 2400,
                Parity = Parity.None,
                StopBits = StopBits.One,
                DataBits = 8,
                Handshake = Handshake.None,
                DtrEnable = true,
                ReadTimeout = 500,
                WriteTimeout = 500,
                PortName = name
            };
            port.DataReceived += _port_DataReceived;

            return port;
        }

        private const int LengthOfMeasurent = 14;
        private readonly List<byte> _buffer = new List<byte>();

        void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            lock (_buffer)
            {
                var currentRead = new byte[_port.BytesToRead];
                _port.Read(currentRead, 0, currentRead.Count());
                _buffer.AddRange(currentRead);
            }

            FindNewMeasureValues();
        }

        private void FindNewMeasureValues()
        {
            for (; ; )
            {
                if (_buffer.Count < LengthOfMeasurent)
                {
                    break;
                }
                var measureValue = new MeasureValue(_buffer.GetRange(0, LengthOfMeasurent));
                _buffer.RemoveRange(0, LengthOfMeasurent);
                if (NewMeasurement != null)
                {
                    NewMeasurement(this, new NewMeasureValueEventArgs(measureValue));
                }
            }

        }

        public event NewMeasureValueEventHandler NewMeasurement;

        private readonly SerialPort _port;
    }
}
