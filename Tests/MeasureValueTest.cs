using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Interface;
using Model;
using NUnit.Framework;

namespace Tests
{
    using Microsoft.FSharp.Core;

    [TestFixture]
    public class MeasureValueTest
    {
        private static readonly byte[] _bufferIsNegativAndIsDc = { 0xe8, 0x17, 0x28, 0x35, 0x45, 0x5b, 0x67, 0x7e, 0x89, 0x95, 0xa0, 0xb8, 0xc0, 0xd4 };
        private static readonly byte[] _bufferIsAC = { 0x1b, 0x27, 0x3d, 0x4f, 0x5d, 0x67, 0x7d, 0x87, 0x9d, 0xa0, 0xb0, 0xc0, 0xd4, 0xe8 };

        [Test]
        public static void IsNegativ()
        {
            var value = AllDisplayedData.GetAllData(_bufferIsNegativAndIsDc).Value.Value;
            value.Should().BeLessThan(0);
        }

        [Test]
        public static void IsDC()
        {
            var value = AllDisplayedData.GetAllData(_bufferIsNegativAndIsDc);
            value.KindOfCurrent.Should().Be(FSharpOption<MeasurementTypes.ACOrDC>.Some(MeasurementTypes.ACOrDC.DC));
        }

        [Test]
        public static void IsAC()
        {
            var value = AllDisplayedData.GetAllData(_bufferIsAC);
            value.KindOfCurrent.Should().Be(FSharpOption<MeasurementTypes.ACOrDC>.Some(MeasurementTypes.ACOrDC.AC));
        }
    }
}
