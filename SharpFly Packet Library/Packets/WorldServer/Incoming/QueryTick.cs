namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class QueryTick
    {
        public uint Time;

        public QueryTick(IncomingPacket packet)
        {
            Time = packet.ReadUInt();
        }
    }
}
