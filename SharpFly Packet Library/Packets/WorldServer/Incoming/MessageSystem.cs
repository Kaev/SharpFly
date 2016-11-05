namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MessageSystem
    {
        public string Message;

        public MessageSystem(IncomingPacket packet)
        {
            Message = packet.ReadString();
        }
    }
}
