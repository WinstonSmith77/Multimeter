using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Interface;

namespace Model
{
    using System.Runtime.DesignerServices;

    public class MeasureValue : IMeasureValue
    {
        private readonly byte[] _bufferDecoded;

        public bool IsNegative
        {
            get
            {
                return ((LowerBits) _bufferDecoded[(int)BytesInTelegram.Two]).HasFlag(LowerBits.Four);
            }
        }

        public bool IsAC
        {
            get
            {
                return ((LowerBits)_bufferDecoded[(int)BytesInTelegram.One]).HasFlag(LowerBits.Four);
            }
        }

        public bool IsDC
        {
            get
            {
                return ((LowerBits)_bufferDecoded[(int)BytesInTelegram.One]).HasFlag(LowerBits.Three);
            }
        }

        public MeasureValue(IEnumerable<byte> buffer)
        {

            _bufferDecoded = VC_840Decoder.Decode(buffer);
            _toString = "new byte[] {" +
                        buffer.ToArray().Aggregate("", (current, value) => current + "0x" + value.ToString("x2") + ",",
                            result => result.TrimEnd(',')) + "}";
        }

        private readonly string _toString;
        public override string ToString()
        {
            return _toString;
        }
    }
}
