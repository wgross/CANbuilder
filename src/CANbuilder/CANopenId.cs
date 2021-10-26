namespace CANbuilder
{
    public struct CANopenId
    {
        /// <summary>
        /// 11 bit CAN open id containing function and node id.
        /// </summary>
        private readonly ushort canOpenId;

        public CANopen.Function Function => (CANopen.Function)(this.canOpenId & 0b_0000_0111_1000_0000);

        public byte NodeId => (byte)(this.canOpenId & 0b_0000_0000_0111_1111);

        public CANopenId(ushort canOpenId) => this.canOpenId = canOpenId;

        public CANopenId SetFunction(CANopen.Function function) => new((ushort)((this.canOpenId & 0b_0000_0000_0011_1111) | (ushort)function));

        public CANopenId SetNodeId(byte nodeId) => new((ushort)((this.canOpenId & 0b_0000_0111_1000_0000) | (int)nodeId));

        public ushort AsUShort() => this.canOpenId;
    }
}