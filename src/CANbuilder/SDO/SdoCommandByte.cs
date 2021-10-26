namespace CANbuilder.Sdo
{
    /// <summary>
    /// The SDO command byte describes the Domain of an SDO request.
    /// It is the first data byte of the CAN frames data payload.
    /// </summary>
    /// <remarks>
    /// 3 Bit: Client command specifier: 1 - Download, 2- Upload
    /// 1 Bit: 0 (toggle bit for receipts)
    /// 2 Bit: The #bytes in data bytes 4-7 that do not contain data (valid if e & s are set)
    /// 1 Bit: Expedited Transfer: 1 - yes, 0 - no
    /// 1 Bit: Indicates that data size is shown in n: 1 - yes, 0 - no
    /// </remarks>
    public readonly struct SdoCommandByte
    {
        public SdoCommandByte(byte commandByte) => this.AsByte = commandByte;

        /// <summary>
        /// The raw byte value of the command byte
        /// </summary>
        public byte AsByte { get; }

        #region Command Specifier

        /// <summary>
        /// Client Command Specifier is 1
        /// </summary>
        public bool IsDownload => (this.AsByte & 0b_0010_0000) == 0b_0010_0000;

        /// <summary>
        /// Client Command Specifier is 2
        /// </summary>
        public bool IsUpload => (this.AsByte & 0b_0100_0000) == 0b_0100_0000;

        /// <summary>
        /// Command Specifier is 8.
        /// </summary>
        public bool IsAbort => (this.AsByte & 0b_1000_0000) == 0b_1000_0000;

        /// <summary>
        /// Set  Command Specifier to 8 == Abort
        /// </summary>
        public SdoCommandByte Abort() => new((byte)((this.AsByte & 0b_0001_1111) | 0b_1000_0000));

        /// <summary>
        /// Set Client Command Specifier to 1
        /// </summary>
        public SdoCommandByte Download() => new((byte)((this.AsByte & 0b_0001_1111) | 0b_0010_0000));

        /// <summary>
        /// Set Client Command Specifier to 2
        /// </summary>
        public SdoCommandByte Upload() => new((byte)((this.AsByte & 0b_0001_1111) | 0b_0100_0000));

        #endregion Command Specifier

        #region Data Size Information

        public SdoCommandByte NumberOfDataBytes(byte num, bool expedited)
        {
            var notUsed = 4 - num & 0b_000_0011;

            if (num == 0)
            {
                // no data is send.
                // n = 0,  e = expedited, s = not valid
                return new((byte)((this.AsByte & 0b_1111_0000) | 0b_0000_0010));
            }
            else if (notUsed == 0)
            {
                // n = 0, s = valid
                var tmp = (byte)((this.AsByte & 0b_1111_0000) | 0b_0000_0001);
                // 1 = 0 oder 1
                return new((byte)(tmp | (expedited ? 0b_0000_0010 : 0b_0000_0000)));
            }
            else
            {
                // some data is sent
                // n = notUsed, e = expedited s = is valid
                var tmp = (this.AsByte & 0b_1111_0000) | 0b_0000_0011;
                return new((byte)(tmp | (notUsed << 2)));
            }
        }

        /// <summary>
        /// Data is complete in this message, <see cref="NumberOfFreeBytes"/> indicates the remaining space
        /// </summary>
        public bool IsExpedited => (this.AsByte & 0b_0000_0010) == 0b_0000_0010;

        /// <summary>
        /// Number of free bytes is valid
        /// </summary>
        public bool IsNumberOfFreeBytesValid => (this.AsByte & 0b_0000_0001) == 0b_0000_001;

        /// <summary>
        /// Number of free bytes left in a message
        /// </summary>
        public byte NumberOfFreeBytes => (byte)((this.AsByte & 0b_0000_1100) >> 2);

        #endregion Data Size Information
    }
}