namespace CANbuilder
{
    public static class CANopen
    {
        public enum SdoCommandByte : byte
        {
            /// <summary>
            /// 1 << 5 = 32 = 0x20  designates a download in the command byte
            /// </summary>
            Download = 0b_0010_0000,

            /// <summary>
            /// 1 << 6 = 64 = 0x40  designates an upload in the command byte
            /// </summary>
            Upload = 0b_0100_0000,

            Expedited = 0b_0000_0010,

            NumberOfFreeBytesIsSet = 0b_0000_0001
        }

        public enum Function : ushort
        { 
            TransmitSdo1 = 0x580,
            
            ReceiveSdo1 = 0x600
        }
    }
}