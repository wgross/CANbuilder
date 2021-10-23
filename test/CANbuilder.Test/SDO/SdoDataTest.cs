using CANbuilder.Sdo;
using System.Linq;
using Xunit;

namespace CANbuilder.Test
{
    public class SdoDataTest
    {
        [Fact]
        public void Create_SDO_download()
        {
            // ACT
            var result = SdoFrames.Download(objectDictionaryIndex: new ObjectDictionaryIndex(index: 0x1006, subindex: 1));

            // ASSERT

            var singleSdoFrame = (SdoCommandFrame)result;

            Assert.True(singleSdoFrame.Command.IsDownload);

            Assert.Equal(0x1006, singleSdoFrame.Index.Index);
            Assert.Equal(1, singleSdoFrame.Index.SubIndex);

            Assert.Equal(8, singleSdoFrame.AsByteArray.Length);
            Assert.False(singleSdoFrame.Command.IsNumberOfFreeBytesValid);
            Assert.True(singleSdoFrame.Command.IsExpedited);

            Assert.Equal(8, singleSdoFrame.AsByteArray.Length);
        }

        [Fact]
        public void Create_SDO_upload_4bytes_expedited()
        {
            // ACT
            var result = SdoFrames.Upload(
                objectDictionaryIndex: new ObjectDictionaryIndex(index: 0x1006, subindex: 1),
                uploadData: new byte[4] { 0x01, 0x02, 0x03, 0x04 }).Single();

            // ASSERT

            var singleSdoFrame = (SdoCommandFrame)result;

            Assert.True(singleSdoFrame.Command.IsUpload);

            Assert.Equal(0x1006, singleSdoFrame.Index.Index);
            Assert.Equal(1, singleSdoFrame.Index.SubIndex);

            Assert.True(singleSdoFrame.Command.IsNumberOfFreeBytesValid);
            Assert.Equal(0, singleSdoFrame.Command.NumberOfFreeBytes);
            Assert.True(singleSdoFrame.Command.IsExpedited);

            Assert.Equal(8, singleSdoFrame.AsByteArray.Length);
            Assert.Equal(1, singleSdoFrame.AsByteArray[4]);
            Assert.Equal(2, singleSdoFrame.AsByteArray[5]);
            Assert.Equal(3, singleSdoFrame.AsByteArray[6]);
            Assert.Equal(4, singleSdoFrame.AsByteArray[7]);
        }

        [Fact]
        public void Create_SDO_upload_2bytes_expedited()
        {
            // ACT
            var result = SdoFrames.Upload(
                objectDictionaryIndex: new ObjectDictionaryIndex(index: 0x1006, subindex: 1),
                uploadData: new byte[2] { 0x01, 0x02 }).Single();

            // ASSERT

            var singleSdoFrame = (SdoCommandFrame)result;

            Assert.True(singleSdoFrame.Command.IsUpload);

            Assert.Equal(0x1006, singleSdoFrame.Index.Index);
            Assert.Equal(1, singleSdoFrame.Index.SubIndex);

            Assert.True(singleSdoFrame.Command.IsNumberOfFreeBytesValid);
            Assert.Equal(2, singleSdoFrame.Command.NumberOfFreeBytes);
            Assert.True(singleSdoFrame.Command.IsExpedited);

            Assert.Equal(8, singleSdoFrame.AsByteArray.Length);
            Assert.Equal(1, singleSdoFrame.AsByteArray[4]);
            Assert.Equal(2, singleSdoFrame.AsByteArray[5]);
            Assert.Equal(0, singleSdoFrame.AsByteArray[6]);
            Assert.Equal(0, singleSdoFrame.AsByteArray[7]);
        }

        [Fact]
        public void Create_SDO_upload_10bytes()
        {
            // ACT
            var result = SdoFrames.Upload(
                objectDictionaryIndex: new ObjectDictionaryIndex(index: 0x1006, subindex: 1),
                uploadData: new byte[10] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x9, 0xA }).ToArray();

            // ASSERT

            var firstSegment = (SdoCommandFrame)result[0];

            Assert.True(firstSegment.Command.IsUpload);

            Assert.Equal(0x1006, firstSegment.Index.Index);
            Assert.Equal(1, firstSegment.Index.SubIndex);

            Assert.True(firstSegment.Command.IsNumberOfFreeBytesValid);
            Assert.Equal(0, firstSegment.Command.NumberOfFreeBytes);
            Assert.False(firstSegment.Command.IsExpedited);

            Assert.Equal(8, firstSegment.AsByteArray.Length);
            Assert.Equal(10, firstSegment.AsByteArray[4]);
            Assert.Equal(0, firstSegment.AsByteArray[5]);
            Assert.Equal(0, firstSegment.AsByteArray[6]);
            Assert.Equal(0, firstSegment.AsByteArray[7]);

            var firstDataSegment = (SdoDataFrame)result[1];

            Assert.Equal(8, firstDataSegment.AsByteArray.Length);
            Assert.Equal(1, firstDataSegment.AsByteArray[0]);
            Assert.Equal(2, firstDataSegment.AsByteArray[1]);
            Assert.Equal(3, firstDataSegment.AsByteArray[2]);
            Assert.Equal(4, firstDataSegment.AsByteArray[3]);
            Assert.Equal(5, firstDataSegment.AsByteArray[4]);
            Assert.Equal(6, firstDataSegment.AsByteArray[5]);
            Assert.Equal(7, firstDataSegment.AsByteArray[6]);
            Assert.Equal(8, firstDataSegment.AsByteArray[7]);

            var secondDataSegment = (SdoDataFrame)result[2];

            Assert.Equal(8, firstDataSegment.AsByteArray.Length);
            Assert.Equal(1, firstDataSegment.AsByteArray[0]);
            Assert.Equal(2, firstDataSegment.AsByteArray[1]);
            Assert.Equal(3, firstDataSegment.AsByteArray[2]);
            Assert.Equal(4, firstDataSegment.AsByteArray[3]);
            Assert.Equal(5, firstDataSegment.AsByteArray[4]);
            Assert.Equal(6, firstDataSegment.AsByteArray[5]);
            Assert.Equal(7, firstDataSegment.AsByteArray[6]);
            Assert.Equal(8, firstDataSegment.AsByteArray[7]);
        }
    }
}