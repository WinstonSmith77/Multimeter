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
        private readonly VC_840Decoder.DecodedBuffer _bufferDecoded;

        public bool IsNegative
        {
            get
            {
                return VC_840Decoder.IsNegative(this._bufferDecoded);
            }
        }

        public bool IsAC
        {
            get
            {
                return VC_840Decoder.IsAC(this._bufferDecoded);
            }
        }

        public bool IsDC
        {
            get
            {
                return VC_840Decoder.IsDC(this._bufferDecoded);
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
