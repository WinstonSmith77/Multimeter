﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.FSharp.Core;

namespace Misc
{
    using System;

    public static class FsharpHelpers
    {
        public static T GetOption<T>(this FSharpOption<T> option, T @default)
        {
            return option.IfHasOption(item => item, @default);
        }

        public static R IfHasOption<T, R>(this FSharpOption<T> option, Func<T, R> doIfHas,  R @default)
        {
            if (FSharpOption<T>.get_IsSome(option))
            {
                return doIfHas(option.Value);
            }
            return @default;
        }

        public static List<T> ToList<T>(this T item)
        {
            return new List<T> { item };
        }

        public static List<T> FirstAsList<T>(this IEnumerable<T> items)
        {
            var first = items.FirstOrDefault();

            if (first != null)
            {
                return first.ToList();
            }
            return new List<T>();
        }
    }
}
