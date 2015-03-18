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


            _port = new SerialPort
            {
                BaudRate = 2400,
                Parity = Parity.None,
                StopBits = StopBits.One,
                DataBits = 8,
                Handshake = Handshake.None,
                DtrEnable = true,
                ReadTimeout = 500,
                WriteTimeout = 500,
                PortName = ports.First()
            };


            _port.PinChanged += _port_PinChanged;
            _port.DataReceived += _port_DataReceived;
            var buffer = new byte[14];
            _port.Open();
            _port.Read(buffer, 0, 0);
        }

        void _port_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
          
        }

        void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var ffgfgf = _port.ReadExisting();
           
        }

        private readonly SerialPort _port;
    }
}
