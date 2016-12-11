using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IMeasureValue
    {
        bool IsNegative { get;}
        bool IsAC { get; }
        bool IsDC { get; }
        double Value { get; }
        string Unit { get; }
    }
}
