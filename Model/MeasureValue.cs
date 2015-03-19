using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;

namespace Model
{
    public class MeasureValue : IMeasureValue
    {
        public byte[] Buffer { get; private set; }

        public MeasureValue(IEnumerable<byte> buffer)
        {
            Buffer = buffer.ToArray();
        }

        public override string ToString()
        {
            return Buffer.Aggregate("", (current, value) => current + value.ToString("x2"));
        }
    }
}
