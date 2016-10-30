namespace SharpFly_Packet_Library.Packets.Interserver.Outgoing
{
    public static class OpCodes
    {
        public const uint REGISTER_CLUSTER_REQUEST = 0x01;
        public const uint REGISTER_NEW_CHANNEL = 0x02;
        public const uint REGISTER_CLUSTER_REQUEST_SUCCESFUL = 0x03;
        public const uint REGISTER_NEW_CHANNEL_SUCCESFUL = 0x04;
    }
}
