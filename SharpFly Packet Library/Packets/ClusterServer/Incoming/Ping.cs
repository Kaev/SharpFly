namespace SharpFly_Packet_Library.Packets.ClusterServer.Incoming
{
    public class Ping
    {
        public int Time { get; private set; }

        public Ping(IncomingPacket packet)
        {
            Time = packet.ReadInt();
        }
    }
}
