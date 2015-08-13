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
    [TestFixture]
    public class MeasureValueTest
    {
        private static readonly byte[] _bufferIsNegativAndIsDc = { 0xe8, 0x17, 0x28, 0x35, 0x45, 0x5b, 0x67, 0x7e, 0x89, 0x95, 0xa0, 0xb8, 0xc0, 0xd4 };
        private static readonly byte[] _bufferIsAC = { 0x1b, 0x27, 0x3d, 0x4f, 0x5d, 0x67, 0x7d, 0x87, 0x9d, 0xa0, 0xb0, 0xc0, 0xd4, 0xe8 };

        [Test]
        public static void IsNegativ()
        {
            IMeasureValue value = new MeasureValue(_bufferIsNegativAndIsDc);
            value.IsNegative.Should().BeTrue();
        }

        [Test]
        public static void IsDC()
        {
            IMeasureValue value = new MeasureValue(_bufferIsNegativAndIsDc);
            value.IsDC.Should().BeTrue();
        }

        [Test]
        public static void IsAC()
        {
            IMeasureValue value = new MeasureValue(_bufferIsAC);
            value.IsAC.Should().BeTrue();
        }
    }
}
