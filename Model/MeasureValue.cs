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
        private readonly Dictionary<byte, byte> _bufferDecoded;

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
            var array = buffer.ToArray();
            if (array.Count() != AccessDevice.LengthOfMeasurent)
            {
                throw new ArgumentException("Buffer lengtth mismatch");
            }

            _bufferDecoded = DecodeBuffer(array);
            _toString = "new byte[] {" +
                        array.Aggregate("", (current, value) => current + "0x" + value.ToString("x2") + ",",
                            result => result.TrimEnd(',')) + "}";
        }

        private Dictionary<byte, byte> DecodeBuffer(IEnumerable<byte> buffer)
        {
            var result = new Dictionary<byte, byte>();

            foreach (var bufferValue in buffer)
            {
                var index = (byte)(bufferValue / (byte)LowerBits.Five);
                var value = (byte)(bufferValue & (byte)LowerBits.All);

                result.Add(index, value);
            }

            return result;
        }

        private readonly string _toString;
        public override string ToString()
        {
            return _toString;
        }
    }
}
