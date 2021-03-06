﻿using System;
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

        public bool IsAC => _data.Current.GetOption(MeasurementData.Current.DC).Equals(MeasurementData.Current.AC);

        public bool IsDC => _data.Current.GetOption(MeasurementData.Current.AC).Equals(MeasurementData.Current.DC);

        public string Unit => _data.Unit.IfHasOption(unit => unit.ToString(), "");
      

        public double Value
        {
            get
            {
                var number = _data.ValueUnscaled.GetOption(double.NaN);
                return number;
            }
        }

        public string Factor => _data.Factor.ToString();

        public MeasureValue(Data.AllDisplayedData data)
        {
            this._data = data;
        }
    }
}
