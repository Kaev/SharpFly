namespace SharpFly_Packet_Library.Packets.LoginServer.Incoming
{
    public static class OpCodes
    {
        public const uint PING = 0x14;
        public const uint RELOG_REQUEST = 0x16;
        public const uint LOGIN_REQUEST = 0xFC;
        public const uint SOCK_FIN = 0xFF;
    }
}
