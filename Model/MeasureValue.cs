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
        private readonly AllDisplayedData.AllDisplayedData _data;


        public bool IsNegative => Value < 0;

        public bool IsAC => _data.KindOfCurrent.GetOption(MeasurementTypes.ACOrDC.DC).Equals(MeasurementTypes.ACOrDC.AC);

        public bool IsDC => _data.KindOfCurrent.GetOption(MeasurementTypes.ACOrDC.AC).Equals(MeasurementTypes.ACOrDC.DC);

        public double Value
        {
            get
            {
                var number = _data.Value.GetOption(double.NaN);
                return number;
            }
        }

        public MeasureValue(AllDisplayedData.AllDisplayedData data)
        {
            this._data = data;
        }
    }
}
