namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MessageShout
    {
        public uint PlayerID;
        public string Message;

        public MessageShout(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            Message = packet.ReadString();
        }
    }
}
