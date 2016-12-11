using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class NewMeasureValueEventArgs : EventArgs
    {
        public MeasureValue Value { get; private set; }

        public NewMeasureValueEventArgs(MeasureValue value)
        {
            Value = value;
        }
    }
}
