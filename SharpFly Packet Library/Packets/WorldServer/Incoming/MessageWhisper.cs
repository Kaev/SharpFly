namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MessageWhisper
    {
        public uint SendingPlayerID;
        public uint ReceivingPlayerID;
        public string Message;

        public MessageWhisper(IncomingPacket packet)
        {
            SendingPlayerID = packet.ReadUInt();
            ReceivingPlayerID = packet.ReadUInt();
            Message = packet.ReadString();
        }
    }
}
