namespace CANbuilder.Sdo
{
    /// <summary>
    /// A SDO Data Frame is used to transfer data from one node to another.
    /// It doesn't contains a <see cref="SdoCommandByte"/> or an <see cref="ObjectDictionaryIndex"/>.
    /// </summary>
    public readonly struct SdoDataFrame : ISdoFrame
    {
        private readonly byte[] sdoData;

        public SdoDataFrame(byte[] sdoData) => this.sdoData = sdoData;

        public byte[] AsByteArray => this.sdoData;
    }
}