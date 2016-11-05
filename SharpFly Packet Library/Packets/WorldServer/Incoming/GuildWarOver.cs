namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildWarOver
    {
        public uint WarID;

        public GuildWarOver(IncomingPacket packet)
        {
            WarID = packet.ReadUInt();
        }
    }
}
