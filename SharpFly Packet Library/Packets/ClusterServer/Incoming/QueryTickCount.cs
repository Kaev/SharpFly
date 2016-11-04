namespace SharpFly_Packet_Library.Packets.ClusterServer.Incoming
{
    public class QueryTickCount
    {
        public uint Time { get; private set; }

        public QueryTickCount(IncomingPacket packet)
        {
            Time = packet.ReadUInt();
        }
    }
}
