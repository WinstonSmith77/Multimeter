using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    using System.Data.Common;

    using FluentAssertions;

    using Microsoft.FSharp.Collections;

    using NUnit.Framework;

    [TestFixture]
    public static class BufferHandlingTest
    {
        private const int embedLength = 5;

        [Test]
        public static void SimpleTest()
        {
            var data = CreateRandomData();
            var buffer = CreateBuffer(data);

            var output = BufferHandling.GetAllValidBlocksFromBuffer(buffer).Item1.First().ToArray();

            output.Should().BeEquivalentTo(buffer);
        }

        [Test]
        public static void TestWithNoise()
        {
            var data = CreateRandomData();
            var buffer = CreateBuffer(data);

            var embeddedBuffer = Embed(buffer);

            var output = BufferHandling.GetAllValidBlocksFromBuffer(embeddedBuffer).Item1.First().ToArray();

            output.Should().BeEquivalentTo(buffer);
        }

        [Test]
        public static void TestWithNoiseAndTwoBuffers()
        {
            var buffer = CreateBuffer(CreateRandomData());
            var embeddedBuffer = Embed(buffer);

            var buffer2 = CreateBuffer(CreateRandomData());
            var embeddedBuffer2 = Embed(buffer2);

            var joinded = Join(embeddedBuffer, embeddedBuffer2);

            var result = BufferHandling.GetAllValidBlocksFromBuffer(joinded).Item1;

            result.First().ToArray().Should().BeEquivalentTo(buffer);
            result.Last().ToArray().Should().BeEquivalentTo(buffer2);
        }

        [Test]
        public static void TestRemainingBuffer()
        {
            var buffer = CreateBuffer(CreateRandomData());
            var embeddedBuffer = Embed(buffer);

            var buffer2 = CreateBuffer(CreateRandomData());
            var embeddedBuffer2 = Embed(buffer2);

            var joinded = Join(embeddedBuffer, embeddedBuffer2);

            var result = BufferHandling.GetAllValidBlocksFromBuffer(joinded).Item2;

            result.Length.Should().Be(embedLength);
        }

        private static IEnumerable<byte> Join(byte[] embeddedBuffer, byte[] embeddedBuffer2)
        {
            var result = new List<byte>();

            result.AddRange(embeddedBuffer);
            result.AddRange(embeddedBuffer2);

            return result.ToArray();
        }

        private static byte[] Embed(byte[] buffer)
        {
            var result = new List<byte>();

            for (int i = 0; i < embedLength; i++)
            {
                result.Add(embedLength * 16 + 4);
            }

            result.AddRange(buffer);

            for (int i = 0; i < embedLength; i++)
            {
                result.Add(embedLength * 16 + 4);
            }


            return result.ToArray();
        }

        private static byte[] CreateBuffer(byte[] data)
        {
            return data.Select((dataItem, index) => (byte)(dataItem + (index + 1) * 16)).ToArray();
        }

        private static byte[] CreateRandomData()
        {
            var random = new Random();
            return Enumerable.Range(0, BufferHandling.NumberOfBytesInTelegram).Select(index => (byte)random.Next(16)).ToArray();
        }
    }
}
