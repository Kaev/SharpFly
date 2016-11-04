namespace SharpFly_Packet_Library.Packets.ClusterServer.Incoming
{
    public class AuthQuery
    {

        public int Value1 { get; set; }
        public int Value2 { get; set; }
        public int Value3 { get; set; }
        public int Value4 { get; set; }

        public AuthQuery(IncomingPacket packet)
        {
            Value1 = packet.ReadInt();
            Value2 = packet.ReadInt();
            Value3 = packet.ReadInt();
            Value4 = packet.ReadInt();
        }
    }
}
