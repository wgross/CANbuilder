using System;

namespace CANbuilder
{
    public class DataFrameBuilder
    {
        private readonly ReverseBitArray dataFrame = new(new byte[7] { 0, 0, 0, 0, 0, 0, 0 });

        private (int size, uint value) objectId;
        private bool identifierExtension;
        private bool remoteTransmissionRequest = false; // dataframe
        private byte[] data;

        public DataFrameBuilder SetObject11BitId(uint objectId)
        {
            this.objectId = (11, objectId);
            this.identifierExtension = false; // 11 bit identifier / base format
            return this;
        }

        public DataFrameBuilder SetData(byte[] data)
        {
            if (data.Length > 4) throw new InvalidOperationException("Number of data bytes <= 4");

            this.data = data;
            return this;
        }

        public byte[] Build()
        {
            this.dataFrame[0] = false; // Start of transmission
            this.SetDataFrame(start: 1, count: this.objectId.size, from: this.objectId.value);
            this.dataFrame[12] = false; // 0 - data frame
            this.dataFrame[13] = this.identifierExtension;
            this.dataFrame[14] = false; // reserved, always 0
            return this.dataFrame.Bytes();
        }

        private void SetDataFrame(int start, int count, uint from)
        {
            int targetStart = start;
            int sourceStart = 32 - count;

            for (int i = 0; i < count; i++)
            {
                this.dataFrame[targetStart + i] = from.GetBit(sourceStart + i);
            }
        }

        private void CopyBits(ushort bits, int start, int length)
        {
            //for (int i = length; i >= 0; i--)
            //{
            //    ushort pointer = (ushort)(1 << i);
            //    if ((pointer & bits) == pointer)
            //        this.data[start + (15 - i)] = true;
            //}
        }
    }
}