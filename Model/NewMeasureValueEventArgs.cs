using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;

namespace Model
{
    public class NewMeasureValueEventArgs : EventArgs
    {
        public IMeasureValue Value { get; private set; }

        public NewMeasureValueEventArgs(IMeasureValue value)
        {
            Value = value;
        }
    }
}
