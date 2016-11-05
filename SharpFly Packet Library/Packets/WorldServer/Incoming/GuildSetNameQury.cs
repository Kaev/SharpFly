namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildSetNameQury
    {
        public uint PlayerID;
        public uint GuildID;
        public string GuildName;
        public byte ID;                     //Not sure what that is for, WorldServer uses it

        public GuildSetNameQury(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            GuildID = packet.ReadUInt();
            GuildName = packet.ReadString();
            ID = packet.ReadByte();
        }
    }
}
