namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildPenyaSalary
    {
        public uint PlayerID;
        public uint GuildID;
        public uint Type;           //Seems to be related to the guild Ranks
        public uint PenyaSent;

        public GuildPenyaSalary(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            GuildID = packet.ReadUInt();
            Type = packet.ReadUInt();
            PenyaSent = packet.ReadUInt();
        }
    }
}
