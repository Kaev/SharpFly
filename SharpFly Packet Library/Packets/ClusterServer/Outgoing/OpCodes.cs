namespace SharpFly_Packet_Library.Packets.ClusterServer.Outgoing
{
    public static class OpCodes
    {
        public const uint QUERY_TICK_COUNT = 0xB;
        public const uint AUTH_QUERY = 0x11;
        public const uint PONG = 0x14;
        public const uint SERVER_IP = 0xF2;
        public const uint CHARACTER_LIST = 0xF3;
        public const uint MESSAGE = 0xFE;
        public const uint WORLD_TRANSFER = 0xFF05;
    }
}
