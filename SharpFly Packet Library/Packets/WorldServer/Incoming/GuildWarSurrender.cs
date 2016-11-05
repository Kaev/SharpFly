namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildWarSurrender
    {
        public uint PlayerID;

        public GuildWarSurrender(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
        }
    }
}
