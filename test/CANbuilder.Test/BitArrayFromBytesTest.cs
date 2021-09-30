using System;
using System.Linq;
using Xunit;

namespace CANbuilder.Test
{
    public class BitArrayFromBytesTest
    {
        [Fact]
        public void Get_bit_count()
        {
            var bytes = new ReverseBitArray(new[] { (byte)1, (byte)0 });

            Assert.Equal(16, bytes.Length);
        }

        [Fact]
        public void Get_last_bit_of_first_byte()
        {
            var bytes = new ReverseBitArray(new[] { (byte)1, (byte)0 });

            Assert.True(bytes[7]);
        }

        [Fact]
        public void Get_rejects_invalid_index()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ReverseBitArray(new byte[0])[0]);
            Assert.Throws<ArgumentOutOfRangeException>(() => new ReverseBitArray(new byte[] { 1 })[-1]);
            Assert.Throws<ArgumentOutOfRangeException>(() => new ReverseBitArray(new byte[] { 1 })[8]);
        }

        [Fact]
        public void Set_rejects_invalid_index()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ReverseBitArray(new byte[0])[0] = true);
            Assert.Throws<ArgumentOutOfRangeException>(() => new ReverseBitArray(new byte[] { 1 })[-1] = true);
            Assert.Throws<ArgumentOutOfRangeException>(() => new ReverseBitArray(new byte[] { 1 })[8] = true);
        }

        [Fact]
        public void Set_last_bit_of_first_byte()
        {
            var bytes = new ReverseBitArray(new[] { (byte)1, (byte)0 });
            bytes[7] = false;

            Assert.False(bytes[7]);
        }

        [Fact]
        public void Get_first_bit_of_second_byte()
        {
            var bytes = new ReverseBitArray(new[] { (byte)0, (byte)0b_1000_0000 });

            Assert.True(bytes[8]);
        }

        [Fact]
        public void Get_bits_as_bool_stream_max()
        {
            var bytes = new ReverseBitArray(new[] { byte.MaxValue, byte.MaxValue }).ToArray();

            Assert.Equal(16, bytes.Length);
            Assert.All(bytes, b => Assert.True(b));
        }

        [Fact]
        public void Get_bits_as_bool_stream_min()
        {
            var bytes = new ReverseBitArray(new[] { byte.MinValue, byte.MinValue }).ToArray();

            Assert.Equal(16, bytes.Length);
            Assert.All(bytes, b => Assert.False(b));
        }
    }
}