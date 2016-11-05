namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildDestroy
    {
        public uint MasterID;

        public GuildDestroy(IncomingPacket packet)
        {
            MasterID = packet.ReadUInt();
        }
    }
}
