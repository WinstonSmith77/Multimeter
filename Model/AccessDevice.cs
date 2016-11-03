using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misc;

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

        void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort)sender;

            var currentRead = new byte[port.BytesToRead];
            port.Read(currentRead, 0, currentRead.Length);

            FindNewMeasureValues(currentRead);
        }

        private void FindNewMeasureValues(byte[] buffer)
        {
            var value = AllDisplayedData.GetAllDataFromBuffer(this._oldBuffer, buffer);
            this._oldBuffer = value.Item2;

            value.Item1.FirstAsList().ForEach(meas => NewMeasurement?.Invoke(this, new NewMeasureValueEventArgs(new MeasureValue(meas))));
        }

        private byte[] _oldBuffer = { };

        public event NewMeasureValueEventHandler NewMeasurement;

    }
}
