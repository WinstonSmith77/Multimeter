using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;
using Model;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MeasureValueTest
    {
        [Test]
        public static void IsNegativ()
        {
            IMeasureValue value = new MeasureValue(new byte[] { 0x3d, 0x43, 0x5f, 0x63, 0x7f, 0x8f, 0x9d, 0xa0, 0xb8, 0xc0, 0xd4, 0xe8, 0x17, 0x2f });
            Assert.IsTrue(value.IsNegative);
        }
    }
}
