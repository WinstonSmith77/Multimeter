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
        [Flags]
        private enum LowerBits : byte
        {
            One = 1,
            Two = 2,
            Three = 4,
            Four = 8,
            All = One | Two | Three | Four,
            Five = 16,
        }

        private enum Bytes
        {
            One = 1,
            Two = 2,
        }


        public byte[] Buffer { get; private set; }
        private readonly Dictionary<byte, byte> _bufferDecoded;

        public bool IsNegative
        {
            get
            {
                return ((LowerBits) _bufferDecoded[(int)Bytes.Two]).HasFlag(LowerBits.Four);
            }
        }

        public bool IsAC
        {
            get
            {
                return ((LowerBits)_bufferDecoded[(int)Bytes.One]).HasFlag(LowerBits.Four);
            }
        }

        public bool IsDC
        {
            get
            {
                return ((LowerBits)_bufferDecoded[(int)Bytes.One]).HasFlag(LowerBits.Three);
            }
        }

        public MeasureValue(IEnumerable<byte> buffer)
        {
            var array = buffer.ToArray();
            if (array.Count() != AccessDevice.LengthOfMeasurent)
            {
                throw new ArgumentException("Buffer lengtth mismatch");
            }

            Buffer = array;
            _bufferDecoded = DecodeBuffer(buffer);
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

        public override string ToString()
        {
            return "new byte[] {" + Buffer.Aggregate("", (current, value) => current + "0x" + value.ToString("x2") + ",", result => result.TrimEnd(',')) + "}";
        }
    }
}
