using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Interface;
using Microsoft.FSharp.Core;
using Misc;

namespace Model
{
    
    public class MeasureValue : IMeasureValue
    {
        private readonly DecoderTypes.DecodedBuffer _bufferDecoded;

        public bool IsNegative => VC_840Decoder.IsNegative(this._bufferDecoded);

        public bool IsAC => VC_840Decoder.KindOfCurrent(this._bufferDecoded).GetOption(DecoderTypes.ACOrDC.DC).Equals(DecoderTypes.ACOrDC.AC);

        public bool IsDC => VC_840Decoder.KindOfCurrent(this._bufferDecoded).GetOption(DecoderTypes.ACOrDC.AC).Equals(DecoderTypes.ACOrDC.DC);

        public double Value
        {
            get
            {
                var number = AllDisplayedData.GetAllData(_bufferRaw).Value.GetOption(double.NaN);
                return number;
            }
        }

        public MeasureValue(IEnumerable<byte> buffer)
        {
            _bufferRaw = buffer;
            _bufferDecoded = VC_840Decoder.Decode(buffer);
            _toString = VC_840Decoder.BufferToString(_bufferDecoded);
        }

        private readonly string _toString;
        private readonly IEnumerable<byte> _bufferRaw;

        public override string ToString()
        {
            return _toString;
        }
    }
}
