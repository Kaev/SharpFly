namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public static class OpCodes
    {
        public const uint GLOBAL_GUILDANDPARTYDATA = 0xFFFFFF38;

        //Connection
        public const uint JOINWORLD = 0xFFFFFF00;                           //From Neuz
        //Party
        public const uint PARTY_MEMBER_INVITE = 0xFFFFFF11;                 //From Neuz
        public const uint PARTY_MEMBER_UNINVITE = 0xFFFFFF12;               //From Neuz
        public const uint PARTY_CHANGE_MODE = 0xFFFFFF19;                   //From Neuz
        public const uint PARTY_CHANGE_ITEMMODE = 0xFFFFFF20;               //From Neuz
        public const uint PARTY_CHANGE_EXPMODE = 0xFFFFFF21;                //From Neuz
        public const uint PARTY_CHANGE_NAME = 0xFFFFFF1A;                   //From Neuz
        public const uint PARTY_CHANGE_LEADER = 0xFFFFFF2F;                 //From Neuz
        public const uint PARTY_SKILL_USE = 0xFFFFFF1B;                     //From Neuz
        //Messenger
        public const uint MESSENGER_FRIEND_ADD = 0xFFFFFF60;                //From Neuz
        public const uint MESSENGER_FRIEND_ADD_REQUEST = 0xFFFFFF6B;        //From Neuz
        public const uint MESSENGER_FRIEND_BLOCK = 0xFFFFFF68;              //From Neuz
        public const uint MESSENGER_FRIEND_REMOVE = 0xFFFFFF6A;             //From Neuz
        public const uint MESSENGER_GETFRIENDSTATE = 0xFFFFFF64;            //From Neuz
        public const uint MESSENGER_SETFRIENDSTATE = 0xFFFFFF67;            //From Neuz
        //Guild
        public const uint GUILD_SET_RANK = 0xF000B026;                      //From Neuz
        public const uint GUILD_PENYASALARY = 0xF000B027;                   //From Neuz
        public const uint GUILD_SET_NAME = 0xF000B032;                      //From Neuz
        public const uint GUILD_DESTROY = 0xFFFFFF32;                       //From Neuz
        public const uint GUILD_MEMBER_INVITE = 0xFFFFFF33;                 //From Neuz
        public const uint GUILD_MEMBER_UNINVITE = 0xFFFFFF34;               //From Neuz
        public const uint GUILD_MEMBER_SET_RANK = 0xFFFFFF3A;               //From Neuz
        public const uint GUILD_MEMBER_SET_CLASS = 0xFFFFFF74;              //From Neuz
        public const uint GUILD_MEMBER_SET_NICKNAME = 0xFFFFFF75;           //From Neuz
        public const uint GUILD_WAR_DECLARE = 0xF000B036;                   //From Neuz
        public const uint GUILD_WAR_ACCEPT = 0xF000B037;                    //From Neuz
        public const uint GUILD_WAR_SURRENDER = 0xF000B047;                 //From Neuz
        public const uint GUILD_TRUCE_ACCEPT = 0xF000B049;                  //From Neuz
        public const uint GUILD_CHANGE_MASTER = 0xF000F000;                 //From Neuz
        //Player
        public const uint PLAYER_MOVE = 0xFFFFFF01;                         //From Neuz
        //Messages
        public const uint MESSAGE_PRIVATE_STATE = 0xF000B007;               //From Neuz
        //Music
        //Environment
        //Configs
        //PVP                
 
    }
}
