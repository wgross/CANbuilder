using Xunit;

namespace CANbuilder.Test
{
    public class SdoDataTest
    {
        [Fact]
        public void Create_Sdo_download()
        {
            // ACT
            var result = SdoData.Download(index: 0x1006, subindex: 1);

            // ASSERT
            Assert.True(result.Command.IsDownload);
            Assert.False(result.Command.IsNumberOfFreeBytesValid);
            Assert.True(result.Command.IsExpedited);
            Assert.Equal(0x1006, result.Index.Index);
            Assert.Equal(1, result.Index.SubIndex);
        }
    }
}