using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CANbuilder.Sdo
{
    public static class SdoFrames
    {
        [StructLayout(LayoutKind.Explicit)]
        private struct IntAsBytes
        {
            [FieldOffset(0)]
            public int Int;

            [FieldOffset(0)]
            public byte Byte0;

            [FieldOffset(1)]
            public byte Byte1;

            [FieldOffset(2)]
            public byte Byte2;

            [FieldOffset(3)]
            public byte Byte3;

            public byte[] AsByteArray => new[] { this.Byte0, this.Byte1, this.Byte2, this.Byte3 };
        }

        /// <summary>
        /// Build a request to download data from the Object dictionary of a CAN bus node.
        /// </summary>
        public static ISdoFrame Download(ObjectDictionaryIndex objectDictionaryIndex)
        {
            var data = new byte[8];

            data[0] = new SdoCommandByte().Download().NumberOfDataBytes(0, expedited: true).AsByte;
            data[1] = objectDictionaryIndex.IndexHighByte;
            data[2] = objectDictionaryIndex.IndexLowByte;
            data[3] = objectDictionaryIndex.SubIndex;

            // the data bytes remain 0

            return new SdoCommandFrame(data);
        }

        /// <summary>
        /// Upload data to an CAN bus node. The creates at least one <see cref="SdoCommandFrame"/> and if the data
        /// is larger than 4 bytes 1 or more <see cref="SdoDataFrame"/>.
        /// </summary>
        public static IEnumerable<ISdoFrame> Upload(ObjectDictionaryIndex objectDictionaryIndex, byte[] uploadData)
        {
            if (uploadData.Length > 4)
            {
                // first item contains the length of the sent data
                yield return CreateUploadLengthCommandFrame(objectDictionaryIndex, uploadData.Length);

                // following item contain the data only.
                int startIndex = 0;
                for (; startIndex < uploadData.Length - 8; startIndex += 8)
                {
                    yield return CreateDataFrame(uploadData.AsSpan(startIndex, 8));
                }

                if (startIndex < uploadData.Length)
                {
                    yield return CreateDataFrame(uploadData.AsSpan(startIndex, uploadData.Length - startIndex));
                }

                yield break;
            }
            else
            {
                yield return CreateCommandFrame(objectDictionaryIndex, uploadData.AsSpan());
            }
        }

        private static SdoCommandFrame CreateCommandFrame(ObjectDictionaryIndex objectDictionaryIndex, ReadOnlySpan<byte> uploadData)
        {
            var sdoData = new byte[8];

            sdoData[0] = new SdoCommandByte().Upload().NumberOfDataBytes((byte)(uploadData.Length), expedited: true).AsByte;
            sdoData[1] = objectDictionaryIndex.IndexHighByte;
            sdoData[2] = objectDictionaryIndex.IndexLowByte;
            sdoData[3] = objectDictionaryIndex.SubIndex;

            uploadData.CopyTo(sdoData.AsSpan(4, 4));

            return new SdoCommandFrame(sdoData);
        }

        private static SdoCommandFrame CreateUploadLengthCommandFrame(ObjectDictionaryIndex objectDictionaryIndex, int uploadInteger)
        {
            // sending the length is done as not expedited while sending an integer would be expedited

            var sdoData = new byte[8];

            sdoData[0] = new SdoCommandByte().Upload().NumberOfDataBytes((byte)(4), expedited: false).AsByte;
            sdoData[1] = objectDictionaryIndex.IndexHighByte;
            sdoData[2] = objectDictionaryIndex.IndexLowByte;
            sdoData[3] = objectDictionaryIndex.SubIndex;

            IntAsBytes intAsBytes = new IntAsBytes { Int = uploadInteger };

            intAsBytes.AsByteArray.AsSpan().CopyTo(sdoData.AsSpan(4, 4));

            return new SdoCommandFrame(sdoData);
        }

        private static SdoDataFrame CreateDataFrame(ReadOnlySpan<byte> span)
        {
            var sdoData = new byte[8];

            span.CopyTo(sdoData);

            return new SdoDataFrame(sdoData);
        }
    }
}