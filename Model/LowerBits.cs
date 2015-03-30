using System;

namespace Model
{
    [Flags]
    internal enum LowerBits : byte
    {
        One = 1,
        Two = 2,
        Three = 4,
        Four = 8,
        All = One | Two | Three | Four,
        Five = 16,
    }
}