namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public static class OpCodes
    {
        public const uint GLOBAL_GUILDANDPARTYDATA = 0xFFFFFF38;
        public const uint GLOBAL_LUA_EVENT_NOTICE = 0xF000F120;
        //Connection
        public const uint QUERYTICK = 0x1;
        public const uint ADDCONNECTION = 0xB;
        public const uint PREJOIN = 0xFF05;
        public const uint JOINWORLD = 0xFFFFFF00;
        public const uint PING = 0x14;
        public const uint MODE_MODIFY = 0xFF00EB;
        public const uint RECHARGE_IDSTACK = 0x4;
        //Party
        public const uint PARTY_MEMBER_INVITE = 0xFFFFFF11;
        public const uint PARTY_MEMBER_UNINVITE = 0xFFFFFF12;
        public const uint PARTY_CHANGE_MODE = 0xFFFFFF19;
        public const uint PARTY_CHANGE_ITEMMODE = 0xFFFFFF20;
        public const uint PARTY_CHANGE_EXPMODE = 0xFFFFFF21;
        public const uint PARTY_CHANGE_NAME = 0xFFFFFF1A;
        public const uint PARTY_CHANGE_LEADER = 0xFFFFFF2F;
        public const uint PARTY_SENDNAME = 0xFFFFFF70;
        public const uint PARTY_EXP = 0xFFFFFF1E;
        public const uint PARTY_REMOVE_POINT = 0xFFFFFF1F;
        public const uint PARTY_SKILL_USE = 0xFFFFFF1B;
        public const uint PARTY_LEVEL = 0xF000B009;
        //Messenger
        public const uint MESSENGER_FRIEND_ADD = 0xFFFFFF60;
        public const uint MESSENGER_FRIEND_ADD_REQUEST = 0xFFFFFF6B;
        public const uint MESSENGER_FRIEND_BLOCK = 0xFFFFFF68;
        public const uint MESSENGER_FRIEND_REMOVE = 0xFFFFFF6A;
        public const uint MESSENGER_GETFRIENDSTATE = 0xFFFFFF64;
        public const uint MESSENGER_SETFRIENDSTATE = 0xFFFFFF67;
        //Guild
        public const uint GUILD_SET_RANK = 0xF000B026;
        public const uint GUILD_PENYASALARY = 0xF000B027;
        public const uint GUILD_SET_NAME = 0xF000B032;
        public const uint GUILD_SET_NAME_QURY = 0x10;
        public const uint GUILD_DESTROY = 0xFFFFFF32;
        public const uint GUILD_MEMBER_INVITE = 0xFFFFFF33;
        public const uint GUILD_MEMBER_UNINVITE = 0xFFFFFF34;
        public const uint GUILD_MEMBER_CHAR_DELETED = 0xF5;
        public const uint GUILD_MEMBER_SET_RANK = 0xFFFFFF3A;
        public const uint GUILD_MEMBER_SET_CLASS = 0xFFFFFF74;
        public const uint GUILD_MEMBER_SET_NICKNAME = 0xFFFFFF75;
        public const uint GUILD_MEMBER_JOIN_DATE = 0xFFFFFF76;
        public const uint GUILD_WAR_DECLARE = 0xF000B036;
        public const uint GUILD_WAR_ACCEPT = 0xF000B037;
        public const uint GUILD_WAR_SURRENDER = 0xF000B047;
        public const uint GUILD_WAR_JOINLEAVE_ARENA = 0xF000B045;
        public const uint GUILD_WAR_MASTER_ABSENT = 0xF000B04B;
        public const uint GUILD_WAR_OVER = 0xF000B04A;
        public const uint GUILD_TRUCE_REQUEST = 0xF000B048;
        public const uint GUILD_TRUCE_ACCEPT = 0xF000B049;
        public const uint GUILD_CHANGE_MASTER = 0xF000F000;
        public const uint GUILD_SET_LOGO = 0xF000B00A;
        public const uint GUILD_SET_NOTICE = 0xF000B00C;
        public const uint GUILD_PENYA = 0xF000B028;
        public const uint GUILD_COMBAT_STATE = 0xF000D026;
        public const uint GUILD_COMBAT_DISBAND_PARTY = 0xF000D029;
        public const uint GUILD_COMBAT_CREATE_PARTY = 0xF000D02A;
        //Player
        public const uint PLAYER_LEAVE = 0xFFFFFF01;
        public const uint PLAYER_DESTROY = 0xF000B003;
        public const uint PLAYER_CHANGE_NAME = 0x12;
        public const uint PLAYER_SUMMON = 0xFF00E4;
        public const uint PLAYER_TELEPORT = 0xFF00E5;
        public const uint PLAYER_KILL = 0xFF00E5;
        public const uint PLAYER_GET_IP = 0xFF00E7;
        public const uint PLAYER_GET_COUNT = 0xFF00E8;
        public const uint PLAYER_GET_CORE_COUNT = 0xFF00E9;
        public const uint PLAYER_BLOCK = 0xFFFFFF5A;
        //Messages
        public const uint MESSAGE_PRIVATE_STATE = 0xF000B007;
        public const uint MESSAGE_PRIVATE = 0xFF00E0;
        public const uint MESSAGE_WHISPER = 0xFF00D4;
        public const uint MESSAGE_SHOUT = 0xFF00E1;
        public const uint MESSAGE_GM = 0xFF00ED;
        public const uint MESSAGE_SYSTEM = 0xFF00EA;
        public const uint MESSAGE_CAPTION = 0xFF00D6;
        public const uint MESSAGE_PARTY = 0xFFFFFF59;
        public const uint MESSAGE_GUILD = 0xFFFFFF39;
        public const uint MESSAGE_QUIZ = 0xFF000002;
        //Music
        public const uint PLAY_MUSIC = 0xFF00E2;
        public const uint PLAY_SOUND = 0xFF00E3;
        //Environment
        public const uint ENVIRONMENT_RAIN_FALL = 0xFFFFFF52;
        public const uint ENVIRONMENT_RAIN_STOP = 0xFFFFFF54;
        //Configs
        public const uint LOAD_CONSTANT = 0xFF09;
        public const uint GAMERATE = 0xFF06;
        public const uint MONSTER_RESPAWN = 0xFF06;
        //PVP
        public const uint BOUNTY_START = 0xFF00F4;
        public const uint BOUNTY_REWARD = 0xFF00F5;
        public const uint PARTY_START_DUEL = 0xFFFFFF2A;
        //Snoop
        public const uint SNOOP_SET = 0x21;
        public const uint SNOOP_SET_GUILD = 0x22;
        public const uint SNOOP_SET_CHAT = 0xFF0000;
    }
}
