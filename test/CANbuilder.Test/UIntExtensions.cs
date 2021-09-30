using System;
using System.Linq;
using Xunit;

namespace CANbuilder.Test
{
    public class UintExtensionsTest
    {
        [Fact]
        public void Reject_wrong_index()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ((uint)0).SetBit(-1, true));
            Assert.Throws<ArgumentOutOfRangeException>(() => ((uint)0).SetBit(32, true));
        }

        [Fact]
        public void Enable_bit()
        {
            Assert.Equal((uint)5, ((uint)4).SetBit(indexFromLeft: 31, true));
        }

        [Fact]
        public void Disable_bit()
        {
            Assert.Equal((uint)4, ((uint)5).SetBit(indexFromLeft: 31, false));
        }

        [Fact]
        public void Keep_enabled_bit()
        {
            Assert.Equal((uint)5, ((uint)5).SetBit(indexFromLeft: 31, true));
        }

        [Fact]
        public void Keep_disabled_bit()
        {
            Assert.Equal((uint)4, ((uint)4).SetBit(indexFromLeft: 31, false));
        }

        [Fact]
        public void Enable_all_bits()
        {
            uint value = 0;
            foreach (var index in Enumerable.Range(0, count: 32))
                value = value.SetBit(index, true);

            Assert.Equal(uint.MaxValue, value);
        }

        [Fact]
        public void Enable_first_bit()
        {
            unchecked
            {
                Assert.Equal((uint)(1 << 31), ((uint)0).SetBit(0, true));
            }
        }

        [Fact]
        public void Enable_last_bit()
        {
            Assert.Equal((uint)1, ((uint)0).SetBit(31, true));
        }

        [Fact]
        public void Get_first_bit()
        {
            unchecked
            {
                Assert.True(((uint)(1 << 31)).GetBit(0));
                Assert.False(((uint)0).GetBit(0));
            }
        }

        [Fact]
        public void Get_last_bit()
        {
            Assert.True(((uint)1).GetBit(31));
            Assert.False(((uint)0).GetBit(31));
        }
    }
}