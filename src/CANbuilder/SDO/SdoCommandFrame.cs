namespace CANbuilder.Sdo
{
    /// <summary>
    /// The SDO command frame an expedited upload or download request or
    /// to start a multi segment upload of data.
    /// It contains always an <see cref="SdoCommandByte"/> and an <see cref="ObjectDictionaryIndex"/>.
    /// </summary>
    public readonly struct SdoCommandFrame : ISdoFrame
    {
        private readonly byte[] sdoData;

        public SdoCommandFrame(byte[] sdoData) => this.sdoData = sdoData;

        /// <inheritdoc/>
        public byte[] AsByteArray => this.sdoData;

        /// <summary>
        /// Read the current value of the command byte.
        /// </summary>
        public SdoCommandByte Command => new(this.sdoData[0]);

        /// <summary>
        /// Read the current value of the object dictionary index
        /// </summary>
        public ObjectDictionaryIndex Index => new(this.sdoData[1..4]);
    }
}