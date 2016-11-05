namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class Ping
    {
        public int TimeSent;

        public Ping(IncomingPacket packet)
        {
            TimeSent = packet.ReadInt();
        }
    }
}
