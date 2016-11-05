namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MessagePrivate
    {
        public uint SendingPlayerID;
        public uint ReceivingPlayerID;
        public string Message;

        public MessagePrivate(IncomingPacket packet)
        {
            SendingPlayerID = packet.ReadUInt();
            ReceivingPlayerID = packet.ReadUInt();
            Message = packet.ReadString();
        }
    }
}
