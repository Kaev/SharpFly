namespace SharpFly_Packet_Library.Packets.ClusterServer.Incoming
{
    public static class OpCodes
    {
        public const uint QUERY_TICK_COUNT = 0x0B;
        public const uint PING = 0x14;
        public const uint CHARACTER_LIST = 0xF6;
        public const uint DELETE_CHARACTER = 0xF5;
        public const uint CREATE_CHARACTER = 0xF4;
        public const uint WORLD_TRANSFER = 0xFF05;
    }
}
