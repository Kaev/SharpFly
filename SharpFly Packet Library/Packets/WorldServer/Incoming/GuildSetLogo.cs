namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildSetLogo
    {
        public uint GuildID;
        public uint PlayerID;
        public uint GuildLogo;

        public GuildSetLogo(IncomingPacket packet)
        {
            GuildID = packet.ReadUInt();
            PlayerID = packet.ReadUInt();
            GuildLogo = packet.ReadUInt();
        }
    }
}
