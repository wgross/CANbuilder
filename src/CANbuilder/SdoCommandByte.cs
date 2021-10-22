namespace CANbuilder
{
    /// <summary>
    /// The SDO command byte describes the Domain of an SDO request.
    /// It is the first data byte of the CAN frames data payload.
    /// </summary>
    public readonly struct SdoCommandByte
    {
        private readonly byte commandByte;

        public SdoCommandByte(byte commandByte) => this.commandByte = commandByte;

        public byte AsByte => this.commandByte;

        #region Client Side Command

        public bool IsDownload => (this.commandByte & 0b_0010_0000) == 0b_0010_0000;

        public bool IsUpload => (this.commandByte & 0b_0100_0000) == 0b_0100_0000;

        public SdoCommandByte Download() => new((byte)((this.commandByte & 0b_0000_1111) | 0b_0010_0000));

        public SdoCommandByte Upload() => new((byte)((this.commandByte & 0b_0000_1111) | 0b_0100_0000));

        #endregion Client Side Command

        #region Data Size Information

        public SdoCommandByte NumberOfDataBytes(byte num, bool expedited)
        {
            var notUsed = 4 - num & 0b_000_0011;

            if (num == 0)
            {
                // no data is send.
                // n = 0,  e = expedited, s = not valid
                return new((byte)((this.commandByte & 0b_1110_0000) | 0b_0000_0010));
            }
            else if (notUsed == 0)
            {
                // n = 0, s = valid
                var tmp = (byte)((this.commandByte & 0b_1110_0000) | 0b_0000_0001);
                // 1 = 0 oder 1
                return new((byte)(tmp | (expedited ? 0b_0000_0010 : 0b_0000_0000)));
            }
            else
            {
                // some data is sent
                // n = notUsed, e = expedited s = is valid
                var tmp = (this.commandByte & 0b_1110_0000) | 0b_0000_0011;
                return new((byte)(tmp | (notUsed << 2)));
            }
        }

        public bool IsExpedited => (this.commandByte & 0b_0000_0010) == 0b_0000_0010;

        public bool IsNumberOfFreeBytesValid => (this.commandByte & 0b_0000_0001) == 0b_0000_001;

        public byte NumberOfFreeBytes => (byte)((this.commandByte & 0b_0000_1100) >> 2);

        #endregion Data Size Information
    }
}