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
    
    public class MeasureValue : IMeasureValue
    {
        private readonly AtomicTypes.DecodedBuffer _bufferDecoded;

        public bool IsNegative => VC_840Decoder.IsNegative(this._bufferDecoded);

        public bool IsAC => VC_840Decoder.KindOfCurrent(this._bufferDecoded).Value.Equals(AtomicTypes.ACOrDC.AC);

        public bool IsDC => VC_840Decoder.KindOfCurrent(this._bufferDecoded).Value.Equals(AtomicTypes.ACOrDC.DC);

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
