using System;
using System.Linq;
using Xunit;

namespace CANbuilder.Test
{
    public class ByteExtensionsTest
    {
        [Fact]
        public void Reject_wrong_index()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ((byte)0).SetBit(-1, true));
            Assert.Throws<ArgumentOutOfRangeException>(() => ((byte)0).SetBit(8, true));
        }

        [Fact]
        public void Enable_bit()
        {
            Assert.Equal(5, ((byte)4).SetBit(indexFromLeft: 7, true));
        }

        [Fact]
        public void Disable_bit()
        {
            Assert.Equal(4, ((byte)5).SetBit(indexFromLeft: 7, false));
        }

        [Fact]
        public void Keep_enabled_bit()
        {
            Assert.Equal(5, ((byte)5).SetBit(indexFromLeft: 7, true));
        }

        [Fact]
        public void Keep_disabled_bit()
        {
            Assert.Equal(4, ((byte)4).SetBit(indexFromLeft: 7, false));
        }

        [Fact]
        public void Enable_all_bits()
        {
            byte value = 0;
            foreach (var index in Enumerable.Range(0, count: 8))
                value = value.SetBit(index, true);

            Assert.Equal(byte.MaxValue, value);
        }

        [Fact]
        public void Enable_first_bit()
        {
            Assert.Equal((byte)0b_10000000, ((byte)0).SetBit(0, true));
        }

        [Fact]
        public void Enable_last_bit()
        {
            Assert.Equal((byte)0b_00000001, ((byte)0).SetBit(7, true));
        }

        [Fact]
        public void Get_first_bit()
        {
            Assert.True(((byte)0b_1000_0000).GetBit(0));
            Assert.False(((byte)0b_0000_0000).GetBit(0));
        }

        [Fact]
        public void Get_last_bit()
        {
            Assert.True(((byte)0b_0000_0001).GetBit(7));
            Assert.False(((byte)0b_0000_0000).GetBit(7));
        }
    }
}