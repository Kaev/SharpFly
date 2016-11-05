namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public static class OpCodes
    {
        public const uint QUERYTICK = 0x00000001;
        public const uint ADDCONNECTION = 0x0000000B;
        public const uint PREJOIN = 0x0000FF05;
        public const uint JOINWORLD = 0xFFFFFF00;
        public const uint LEAVE_PLAYER = 0xFFFFFF01;
        public const uint DESTROY_PLAYER = 0xF000B003;
        public const uint PING = 0x14;
        //Party
        public const uint PARTY_MEMBERINVITE = 0xFFFFFF11;
        public const uint PARTY_MEMBERUNINVITE = 0xFFFFFF12;
        public const uint PARTY_CHANGEMODE = 0xFFFFFF19;
        public const uint PARTY_CHANGEITEMMODE = 0xFFFFFF20;
        public const uint PARTY_CHANGEEXPMODE = 0xFFFFFF21;
        public const uint PARTY_CHANGENAME = 0xFFFFFF1A;
        public const uint PARTY_SENDNAME = 0xFFFFFF70;
        public const uint PARTY_CHANGELEADER = 0xFFFFFF2F;
        //Messenger
        public const uint MESSENGER_ADDFRIEND = 0xFFFFFF60;
        public const uint MESSENGER_GETFRIENDSTATE = 0xFFFFFF64;
        public const uint MESSENGER_SETFRIENDSTATE = 0xFFFFFF67;
        public const uint MESSENGER_BLOCKFRIEND = 0xFFFFFF68;
        public const uint MESSENGER_REMOVEFRIEND = 0xFFFFFF6A;
        //Guild
        public const uint GUILD_SETRANK = 0xF000B026;
        public const uint GUILD_PENYASALARY = 0xF000B027;
        public const uint GUILD_SETNAME = 0xF000B032;
        public const uint GUILD_DESTROY = 0xFFFFFF32;
        public const uint GUILD_MEMBERINVITE = 0xFFFFFF33;
        public const uint GUILD_MEMBERUNINVITE = 0xFFFFFF34;
        public const uint GUILD_MEMBERSETRANK = 0xFFFFFF3A;
        public const uint GUILD_MEMBERSETCLASS = 0xFFFFFF74;
        public const uint GUILD_MEMBERSETNICKNAME = 0xFFFFFF75;
        public const uint GUILD_DECLAREWAR = 0xF000B036;
        public const uint GUILD_ACCEPTWAR = 0xF000B037;
        public const uint GUILD_SURRENDERWAR = 0xF000B047;
        public const uint GUILD_TRUCEREQUEST = 0xF000B048;
        public const uint GUILD_TRUCEACCEPT = 0xF000B049;
        public const uint GUILD_CHANGEMASTER = 0xF000F000;


        public const uint GLOBAL_GUILDANDPARTYDATA = 0xFFFFFF38;

        public const uint PRIVATEMESSAGE = 0xF000B007;
    }
}
