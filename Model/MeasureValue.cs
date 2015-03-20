using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Interface;

namespace Model
{
    public class MeasureValue : IMeasureValue
    {
        public byte[] Buffer { get; private set; }

        public bool IsNegative
        {
            get
            {
                return true;
            }
        }

        public MeasureValue(IEnumerable<byte> buffer)
        {
            if (buffer.Count() != AccessDevice.LengthOfMeasurent)
            {
                throw new ArgumentException("Buffer lengtth mismatch");
            }

            Buffer = buffer.ToArray();
        }

        public override string ToString()
        {
            return "new byte[] {" + Buffer.Aggregate("", (current, value) => current + "0x" + value.ToString("x2") + ",", result => result.TrimEnd(',')) + "}";
        }
    }
}
