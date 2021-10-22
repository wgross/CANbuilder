using Xunit;

namespace CANbuilder.Test
{
    public class ObjectDictionaryIndexTest
    {
        [Fact]
        public void Create_from_numbers()
        {
            // ACT
            var result = new ObjectDictonaryIndex(0x0102, 3);

            // ASSERT
            Assert.Equal(0x0102, result.Index);
            Assert.Equal(3, result.SubIndex);
            Assert.Equal(0x01, result.IndexHighByte);
            Assert.Equal(0x02, result.IndexLowByte);
        }

        [Fact]
        public void Create_from_bytes()
        {
            // ACT
            var result = new ObjectDictonaryIndex(new byte[] { 0x01, 0x02, 3 });

            // ASSERT
            Assert.Equal(0x0102, result.Index);
            Assert.Equal(3, result.SubIndex);
            Assert.Equal(0x01, result.IndexHighByte);
            Assert.Equal(0x02, result.IndexLowByte);
        }

        [Fact]
        public void Change_subindex()
        {
            // ARRANGE
            var index = new ObjectDictonaryIndex(0x0102, 3);

            // ACT
            var result = new ObjectDictonaryIndex(0x0102, 3).SetSubIndex(4);

            // ASSERT
            Assert.Equal(0x0102, result.Index);
            Assert.Equal(4, result.SubIndex);
            Assert.Equal(0x01, result.IndexHighByte);
            Assert.Equal(0x02, result.IndexLowByte);
        }

        [Fact]
        public void Change_index()
        {
            // ARRANGE
            var index = new ObjectDictonaryIndex(0x0102, 3);

            // ACT
            var result = new ObjectDictonaryIndex(0x0102, 3).SetIndex(0x0203);

            // ASSERT
            Assert.Equal(0x0203, result.Index);
            Assert.Equal(3, result.SubIndex);
            Assert.Equal(0x02, result.IndexHighByte);
            Assert.Equal(0x03, result.IndexLowByte);
        }
    }
}