namespace SharpFly_Packet_Library.Packets.Interserver.Incoming
{
    public static class OpCodes
    {
        public const uint REGISTER_CLUSTER_REQUEST = 0x1;
        public const uint REGISTER_NEW_CHANNEL = 0x2;
        public const uint REGISTER_CLUSTER_REQUEST_SUCCESFUL = 0x3;
        public const uint REGISTER_NEW_CHANNEL_SUCCESFUL = 0x4;
        public const uint REGISTER_CHANNEL_REQUEST = 0x5;
        public const uint REGISTER_CHANNEL_REQUEST_SUCCESFUL = 0x6;
    }
}
