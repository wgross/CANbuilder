using System;
using Xunit;

namespace CANbuilder.Test
{
    public class CANBuilderTest
    {
        private readonly DataFrameBuilder builder;

        public CANBuilderTest()
        {
            this.builder = new DataFrameBuilder();
        }

        [Fact]
        public void Datagram_begins_with_0()
        {
            // ACT
            var result = this.builder.Build();

            // ASSERT
            var datagram = new ReverseBitArray(result);

            Assert.False(datagram[0]);
        }

        [Fact]
        public void Datagram_has_11_bit_id_at_idx_1()
        {
            // ACT
            var result = this.builder.SetObject11BitId(1).Build();

            // ASSERT
            var bitArray = new ReverseBitArray(result);

            Assert.False(bitArray[1]);
            Assert.False(bitArray[2]);
            Assert.False(bitArray[3]);
            Assert.False(bitArray[4]);
            Assert.False(bitArray[5]);
            Assert.False(bitArray[6]);
            Assert.False(bitArray[7]);
            Assert.False(bitArray[8]);
            Assert.False(bitArray[9]);
            Assert.False(bitArray[10]);
            Assert.True(bitArray[11]);
        }

        [Fact]
        public void Datagram_has_no_remote_frame_request()
        {
            // ACT
            var result = this.builder.SetObject11BitId(1).Build();

            // ASSERT
            var bitArray = new ReverseBitArray(result);

            Assert.False(bitArray[12]);
        }

        [Fact]
        public void Datagram_has_reserved_bit_r0_zero()
        {
            // ACT
            var result = this.builder.SetObject11BitId(1).Build();

            // ASSERT
            var bitArray = new ReverseBitArray(result);

            Assert.False(bitArray[13]);
        }

        [Fact]
        public void Datagram_has_data_length_0()
        {
            // ACT
            var result = this.builder.SetObject11BitId(1).Build();

            // ASSERT
            var bitArray = new ReverseBitArray(result);

            Assert.False(bitArray[14]);
            Assert.False(bitArray[15]);
            Assert.False(bitArray[16]);
            Assert.False(bitArray[17]);
        }

        [Fact]
        public void Reject_invalid_data()
        {
            Assert.Throws<InvalidOperationException>(() => this.builder.SetData(new byte[5]));
        }
    }
}