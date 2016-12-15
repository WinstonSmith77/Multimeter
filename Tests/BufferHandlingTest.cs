using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    using FluentAssertions;

    using Microsoft.FSharp.Collections;

    using NUnit.Framework;

    [TestFixture]
    public static class BufferHandlingTest
    {
        [Test]
        public static void SimpleTest()
        {
            var data = CreateRandomData();
            var buffer = data.Select((dataItem, index) => (byte)(dataItem + (index + 1) * 16)).ToArray();

            var output = BufferHandling.GetAllValidBlocksFromBuffer(buffer).Item1.First().ToArray();

            output.Should().BeEquivalentTo(buffer);
        }

        private static byte[] CreateRandomData()
        {
            var random = new Random();

            return Enumerable.Range(0, BufferHandling.NumberOfBytesInTelegram).Select(index => (byte)random.Next(16)).ToArray();
        }
    }
}
