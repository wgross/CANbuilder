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

        private static ISdoFrame UploadLength(ObjectDictionaryIndex objectDictionaryIndex, int uploadInteger)
        {
            // sending the length is done as not expedited whie sening an integer would be expedited

            var sdoCommand = new SdoCommandByte().Upload().NumberOfDataBytes((byte)(4), expedited: false);

            var sdoData = new byte[8];
            sdoData[0] = sdoCommand.AsByte;
            sdoData[1] = objectDictionaryIndex.IndexHighByte;
            sdoData[2] = objectDictionaryIndex.IndexLowByte;
            sdoData[3] = objectDictionaryIndex.SubIndex;

            IntAsBytes intAsBytes = new IntAsBytes { Int = uploadInteger };

            Array.Copy(
                sourceArray: new IntAsBytes { Int = uploadInteger }.AsByteArray,
                sourceIndex: 0,
                destinationArray: sdoData,
                destinationIndex: 4,
                length: 4);

            return new SdoCommandFrame(sdoData);
        }

        /// <summary>
        /// Upload data to an CAN bus node. The creates at least one <see cref="SdoCommandFrame"/> and if the data
        /// is larger than 4 bytes 1 oro more <see cref="SdoDataFrame"/>.
        /// </summary>
        public static IEnumerable<ISdoFrame> Upload(ObjectDictionaryIndex objectDictionaryIndex, byte[] uploadData)
        {
            var sdoCommand = new SdoCommandByte().Upload().NumberOfDataBytes((byte)(uploadData.Length), expedited: true);

            if (uploadData.Length > 4)
            {
                // first item contains the length of the sent data
                yield return UploadLength(objectDictionaryIndex, uploadData.Length);

                int numberOfDataFrames = Math.DivRem(uploadData.Length, 8, out var bytesInLastFrame);

                for (int frameNumber = 0; frameNumber < numberOfDataFrames; frameNumber++)
                {
                    yield return UploadDataFrame(frameNumber, uploadData, 8);
                }

                if (bytesInLastFrame > 0)
                {
                    yield return UploadDataFrame(numberOfDataFrames, uploadData, bytesInLastFrame);
                }
            }
            else
            {
                var sdoData = new byte[8];
                sdoData[0] = sdoCommand.AsByte;
                sdoData[1] = objectDictionaryIndex.IndexHighByte;
                sdoData[2] = objectDictionaryIndex.IndexLowByte;
                sdoData[3] = objectDictionaryIndex.SubIndex;

                Array.Copy(uploadData, sourceIndex: 0, sdoData, destinationIndex: 4, length: uploadData.Length);

                yield return new SdoCommandFrame(sdoData);
            }
        }

        private static SdoDataFrame UploadDataFrame(int frameNumber, byte[] uploadData, int numberOfBytes)
        {
            var sdoData = new byte[8];

            Array.Copy(
                sourceArray: uploadData,
                sourceIndex: 8 * frameNumber,
                destinationArray: sdoData,
                destinationIndex: 0,
                length: numberOfBytes);

            return new SdoDataFrame(sdoData);
        }
    }
}