namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildSetName
    {
        public uint PlayerID;
        public uint GuildID;
        public string GuildName;

        public GuildSetName(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            GuildID = packet.ReadUInt();
            GuildName = packet.ReadString();
        }
    }
}
