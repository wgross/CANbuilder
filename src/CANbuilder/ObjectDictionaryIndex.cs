namespace CANbuilder
{
    public struct ObjectDictionaryIndex
    {
        private readonly ushort index;
        private readonly byte subindex;

        public ObjectDictionaryIndex(ushort index, byte subindex)
        {
            this.index = index;
            this.subindex = subindex;
        }

        public ObjectDictionaryIndex(byte[] indexAsByteArray) : this()
        {
            this.index = (ushort)((ushort)(indexAsByteArray[0] << 8) | indexAsByteArray[1]);
            this.subindex = indexAsByteArray[2];
        }

        public ushort Index => this.index;

        public byte SubIndex => this.subindex;

        public byte IndexLowByte => (byte)(this.index & 0x00FF);

        public byte IndexHighByte => (byte)((this.index & 0xFF00) >> 8);

        public ObjectDictionaryIndex SetSubIndex(byte subIndex) => new(this.index, subIndex);

        public ObjectDictionaryIndex SetIndex(ushort index) => new(index, this.subindex);
    }
}