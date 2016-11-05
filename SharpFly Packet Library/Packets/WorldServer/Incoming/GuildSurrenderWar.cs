namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildSurrenderWar
    {
        public uint PlayerID;

        public GuildSurrenderWar(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
        }
    }
}
