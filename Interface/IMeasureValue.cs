﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IMeasureValue
    {
        byte[] Buffer { get; }
        bool IsNegative { get;}
    }
}
