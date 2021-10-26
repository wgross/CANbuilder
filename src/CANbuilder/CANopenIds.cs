namespace CANbuilder
{
    public static class CANopenIds
    {
        /// <summary>
        /// Creates a Transmit SDO object id to the given <paramref name="nodeId"/>.
        /// </summary>
        public static CANopenId TransitSdo(byte nodeId) => new CANopenId().SetFunction(CANopen.Function.TransmitSdo1).SetNodeId(nodeId);

        /// <summary>
        /// Creates a Receive SDO object id to the given <paramref name="nodeId"/>.
        /// </summary>
        public static CANopenId ReceiveSdo1(byte nodeId) => new CANopenId().SetFunction(CANopen.Function.ReceiveSdo1).SetNodeId(nodeId);
    }
}