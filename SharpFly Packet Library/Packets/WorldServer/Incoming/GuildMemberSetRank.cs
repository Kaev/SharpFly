namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildMemberSetRank
    {
        public uint MasterID;
        public uint PlayerID;
        public int Rank;

        public GuildMemberSetRank(IncomingPacket packet)
        {
            MasterID = packet.ReadUInt();
            PlayerID = packet.ReadUInt();
            Rank = packet.ReadInt();
        }
    }
}
