using Xunit;

namespace CANbuilder.Test
{
    public class SdoCommandByteTest
    {
        [Fact]
        public void CommandByte_for_download()
        {
            // ARRANGE
            SdoCommandByte commandByte = default;

            // ACT
            var result = commandByte.Download();

            // ASSERT
            Assert.Equal(0b_0010_0000, result.AsByte);
            Assert.True(result.IsDownload);
            Assert.False(result.IsUpload);
            Assert.False(result.IsExpedited);
            Assert.False(result.IsNumberOfFreeBytesValid);
            Assert.Equal(0, result.NumberOfFreeBytes);
        }

        [Fact]
        public void CommandByte_for_upload()
        {
            // ARRANGE
            SdoCommandByte commandByte = default;

            // ACT
            var result = commandByte.Upload();

            // ASSERT
            Assert.Equal(0b_0100_0000, result.AsByte);
            Assert.True(result.IsUpload);
            Assert.False(result.IsDownload);
            Assert.False(result.IsExpedited);
            Assert.False(result.IsNumberOfFreeBytesValid);
            Assert.Equal(0, result.NumberOfFreeBytes);
        }

        [Fact]
        public void CommandByte_for_upload_switch_to_download()
        {
            // ARRANGE
            SdoCommandByte commandByte = default;

            // ACT
            var result = commandByte.Upload().Download();

            // ASSERT
            Assert.Equal(0b_0010_0000, result.AsByte);
            Assert.True(result.IsDownload);
            Assert.False(result.IsUpload);
            Assert.False(result.IsExpedited);
            Assert.False(result.IsNumberOfFreeBytesValid);
            Assert.Equal(0, result.NumberOfFreeBytes);
        }

        [Fact]
        public void CommandByte_for_download_switch_to_upload()
        {
            // ARRANGE
            SdoCommandByte commandByte = default;

            // ACT
            var result = commandByte.Download().Upload();

            // ASSERT
            Assert.Equal(0b_0100_0000, result.AsByte);
            Assert.True(result.IsUpload);
            Assert.False(result.IsDownload);
            Assert.False(result.IsExpedited);
            Assert.False(result.IsNumberOfFreeBytesValid);
            Assert.Equal(0, result.NumberOfFreeBytes);
        }

        [Fact]
        public void CommandByte_has_4_bytes_and_is_expedited()
        {
            // ARRANGE
            SdoCommandByte commandByte = default;

            // ACT
            var result = commandByte.NumberOfDataBytes(4, true);

            // ASSERT
            Assert.Equal(0b_0000_0011, result.AsByte);
            Assert.False(result.IsUpload);
            Assert.False(result.IsDownload);
            Assert.True(result.IsExpedited);
            Assert.True(result.IsNumberOfFreeBytesValid);
            Assert.Equal(0, result.NumberOfFreeBytes);
        }

        [Fact]
        public void CommandByte_has_4_bytes_and_is_not_expedited()
        {
            // ARRANGE
            SdoCommandByte commandByte = default;

            // ACT
            var result = commandByte.NumberOfDataBytes(4, false);

            // ASSERT
            Assert.Equal(0b_0000_0001, result.AsByte);
            Assert.False(result.IsUpload);
            Assert.False(result.IsDownload);
            Assert.False(result.IsExpedited);
            Assert.True(result.IsNumberOfFreeBytesValid);
            Assert.Equal(0, result.NumberOfFreeBytes);
        }

        [Fact]
        public void CommandByte_has_2_bytes_and_is_expedited()
        {
            // ARRANGE
            SdoCommandByte commandByte = default;

            // ACT
            var result = commandByte.NumberOfDataBytes(2, false);

            // ASSERT
            Assert.Equal(0b_0000_1011, result.AsByte);
            Assert.False(result.IsUpload);
            Assert.False(result.IsDownload);
            Assert.True(result.IsExpedited);
            Assert.True(result.IsNumberOfFreeBytesValid);
            Assert.Equal(2, result.NumberOfFreeBytes);
        }

        [Fact]
        public void CommandByte_has_0_bytes_and_is_expedited()
        {
            // ARRANGE
            SdoCommandByte commandByte = default;

            // ACT
            var result = commandByte.NumberOfDataBytes(0, true);

            // ASSERT
            Assert.Equal(0b_0000_0010, result.AsByte);
            Assert.True(result.IsExpedited);
            Assert.False(result.IsNumberOfFreeBytesValid);
        }
    }
}