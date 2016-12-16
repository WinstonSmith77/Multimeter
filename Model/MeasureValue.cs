using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.FSharp.Core;
using Misc;

namespace Model
{
    
    public class MeasureValue 
    {
        private readonly Data.AllDisplayedData _data;


        public bool IsNegative => Value < 0;

        public bool IsAC => _data.Current.GetOption(MeasurementTypes.Current.DC).Equals(MeasurementTypes.Current.AC);

        public bool IsDC => _data.Current.GetOption(MeasurementTypes.Current.AC).Equals(MeasurementTypes.Current.DC);

        public string Unit => _data.Unit.Text;
      

        public double Value
        {
            get
            {
                var number = _data.ValueUnscaled.GetOption(double.NaN);
                return number;
            }
        }

        public string Factor => _data.Factor.Text;

        public MeasureValue(Data.AllDisplayedData data)
        {
            this._data = data;
        }
    }
}
