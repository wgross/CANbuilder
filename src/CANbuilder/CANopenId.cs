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

        //{
        //    //uint canOpenId = 0;
        //    //const uint null_node_id_bits = 0xFFFFFF80;
        //    //const uint null_not_node_id_bits = ~null_node_id_bits;
        //    //canOpenId = (canOpenId & null_node_id_bits) | (this.NodeId & null_not_node_id_bits);

        //    //const uint null_function_bits = 0xFFFFF87F;
        //    //const uint null_not_function_id_bits = ~null_function_bits;
        //    //canOpenId = (canOpenId & null_function_bits) | (this.Function & null_not_function_id_bits);

        //    return canOpenId;
        //}

        //public (byte[] oid, byte[] data) DataFrames()
        //{
        //    var header = new byte[8];
        //    var funcAndNodeId = this.function + this.nodeId;

        //    header[0] = (byte)(funcAndNodeId >> 3 & 0b_1111_1111);
        //    header[1] = (byte)((funcAndNodeId & 0x7) << 5);

        //    // CAN message may carry 8 bytes of data
        //    // first 3 are index and sub index of dictionary object
        //    var payload = new byte[8];
        //    payload[0] = (byte)((this.index >> 8) & 0xF);
        //    payload[1] = (byte)(this.index & 0xF);
        //    payload[2] = (byte)(this.subindex & 0xF);

        //    return (header, payload);
        //}

        //public CANopenId ObjectDictionary(ushort index, byte subindex)
        //{
        //    this.index = index;
        //    this.subindex = subindex;

        //    return this;
        //}

        //public CANopenId Data(byte[] data)
        //{
        //    this.data = data;

        //    return this;
        //}
    }
}