namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildWarAccept
    {
        public uint MasterID;
        public uint AcceptingGuildID;

        public GuildWarAccept(IncomingPacket packet)
        {
            MasterID = packet.ReadUInt();
            AcceptingGuildID = packet.ReadUInt();
        }
    }
}
