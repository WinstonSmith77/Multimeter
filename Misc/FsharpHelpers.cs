using Microsoft.FSharp.Core;

namespace Misc
{
    public static class FsharpHelpers
    {
        public static T GetOption<T>(this FSharpOption<T> option, T @default)
        {
            if (FSharpOption<T>.get_IsSome(option))
            {
                return option.Value;
            }
            return @default;
        }
    }
}
