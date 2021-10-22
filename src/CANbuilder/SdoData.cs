namespace CANbuilder
{
    public class SdoData
    {
        private byte[] sdoData;

        public SdoData(byte[] data)
        {
            this.sdoData = data;
        }

        public static SdoData Download(ushort index, byte subindex)
        {
            var sdoCommand = new SdoCommandByte().Download().NumberOfDataBytes(0, expedited: true);
            var odIndex = new ObjectDictonaryIndex(index, subindex);

            var data = new byte[8];
            data[0] = sdoCommand.AsByte;
            data[1] = odIndex.IndexHighByte;
            data[2] = odIndex.IndexLowByte;
            data[3] = odIndex.SubIndex;

            return new SdoData(data);
        }

        public byte[] AsByteArray => this.sdoData;

        /// <summary>
        /// Read the current value of the command byte.
        /// </summary>
        public SdoCommandByte Command => new(this.sdoData[0]);


        public ObjectDictonaryIndex Index => new(this.sdoData[1..4]);
    }
}