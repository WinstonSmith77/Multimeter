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

            if (ports.Length != 1)
            {
                throw new ArgumentException("Wrong number of ports!");
            }

            var port = InitComPort(ports.First());
            port.Open();
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

       
        private readonly List<byte> _buffer = new List<byte>();

        void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort) sender;

            lock (_buffer)
            {
                var currentRead = new byte[port.BytesToRead];
                port.Read(currentRead, 0, currentRead.Count());
                _buffer.AddRange(currentRead);
            }

            FindNewMeasureValues();
        }

        private void FindNewMeasureValues()
        {
            for (; ; )
            {
                if (_buffer.Count < VC_840Decoder.numberOfBytesInTelegram)
                {
                    break;
                }
                var measureValue = new MeasureValue(_buffer.GetRange(0, VC_840Decoder.numberOfBytesInTelegram));
                _buffer.RemoveRange(0, VC_840Decoder.numberOfBytesInTelegram);
                NewMeasurement?.Invoke(this, new NewMeasureValueEventArgs(measureValue));
            }

        }

        public event NewMeasureValueEventHandler NewMeasurement;

    }
}
