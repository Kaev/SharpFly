namespace SharpFly_Packet_Library.Packets.ClusterServer.Incoming
{
    public class QueryTickCount
    {
        public int Time { get; private set; }

        public QueryTickCount(IncomingPacket packet)
        {
            Time = packet.ReadInt();
        }
    }
}
