namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildAcceptWar
    {
        public uint MasterID;
        public uint AcceptingGuildID;

        public GuildAcceptWar(IncomingPacket packet)
        {
            MasterID = packet.ReadUInt();
            AcceptingGuildID = packet.ReadUInt();
        }
    }
}
