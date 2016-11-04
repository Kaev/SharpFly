namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class JoinWorld
    {
        public uint WorldID;
        public uint PlayerID;
        public uint AuthKey;
        public uint PartyID;
        public uint GuildID;
        public uint GuildWarID;
        public uint IdOfMulti;      //ID of the channel (When using multiple chanels)?
        public byte CharacterSlot;
        public string CharacterName;
        public string AccountName;
        public string Password;
        public uint MessengerState;
        public int FriendsSize;
        public uint FriendID;
        //TODO: Friend f;

        public JoinWorld(IncomingPacket packet)
        {
            WorldID = packet.ReadUInt();
            PlayerID = packet.ReadUInt();
            AuthKey = packet.ReadUInt();
            PartyID = packet.ReadUInt();
            GuildID = packet.ReadUInt();
            GuildWarID = packet.ReadUInt();
            IdOfMulti = packet.ReadUInt();
            CharacterSlot = packet.ReadByte();
            CharacterName = packet.ReadString();
            AccountName = packet.ReadString();
            Password = packet.ReadString();
            MessengerState = packet.ReadUInt();
            FriendsSize = packet.ReadInt();
            FriendID = packet.ReadUInt();

            for (int i = 0; i < FriendsSize; i++)
            {
                //TODO: packet.Read( &f, sizeof(Friend) );
                //SetFriend( FriendID, &f)
            }
        }
    }
}
