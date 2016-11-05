namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public static class OpCodes
    {
        public const uint JOINWORLD = 0xFFFFFF00;
        public const uint DESTROY_PLAYER = 0xF000B003;
        public const uint PING = 0x14;
        //Party
        public const uint PARTY_MEMBERINVITE = 0xFFFFFF11;
        public const uint PARTY_MEMBERUNINVITE = 0xFFFFFF12;
        public const uint PARTY_CHANGEMODE = 0xFFFFFF19;
        public const uint PARTY_CHANGEITEMMODE = 0xFFFFFF20;
        public const uint PARTY_CHANGEEXPMODE = 0xFFFFFF21;
        public const uint PARTY_CHANGENAME = 0xFFFFFF1A;
        //Messenger
        public const uint MESSENGER_ADDFRIEND = 0xFFFFFF60;
    }
}
