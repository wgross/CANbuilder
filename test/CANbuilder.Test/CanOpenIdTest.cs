using Xunit;

namespace CANbuilder.Test
{
    public class CanOpenIdTest
    {
        [Fact]
        public void Create_TransmitSdo1()
        {
            // ACT
            var result = CANopenIds.TransitSdo(0);

            // ASSERT
            Assert.Equal<ushort>(0x580, result.AsUShort());
            Assert.Equal(CANopen.Function.TransmitSdo1, result.Function);
            Assert.Equal(0, result.NodeId);
        }

        [Fact]
        public void Create_ReceiveSdo1()
        {
            // ACT
            var result = CANopenIds.ReceiveSdo1(0);

            // ASSERT
            Assert.Equal<ushort>(0x600, result.AsUShort());
            Assert.Equal(CANopen.Function.ReceiveSdo1, result.Function);
            Assert.Equal(0, result.NodeId);
        }

        [Fact]
        public void Create_NodeId()
        {
            // ARRANGE
            var canOpenId = new CANopenId();

            // ACT
            var result = canOpenId.SetNodeId(5);

            // ASSERT
            Assert.Equal<ushort>(5, result.AsUShort());
            Assert.Equal(5, result.NodeId);
            Assert.Equal<ushort>(0, (ushort)result.Function);
        }

        [Fact]
        public void Create_NodeId_and_Function()
        {
            // ARRANGE
            var canOpenId = new CANopenId();

            // ACT
            var result = canOpenId.SetNodeId(5).SetFunction(CANopen.Function.ReceiveSdo1);

            // ASSERT
            Assert.Equal(0x605, result.AsUShort());
            Assert.Equal(5, result.NodeId);
            Assert.Equal(CANopen.Function.ReceiveSdo1, result.Function);
        }
    }
}