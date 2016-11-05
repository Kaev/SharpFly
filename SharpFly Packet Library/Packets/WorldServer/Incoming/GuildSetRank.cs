namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildSetRank
    {
        public uint PlayerID;
        public uint GuildID;
        public uint Rank;

        public GuildSetRank(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            GuildID = packet.ReadUInt();
            Rank = packet.ReadUInt();
        }
    }
}
