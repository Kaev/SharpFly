namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MessageGM
    {
        public uint PlayerID;
        public uint WorldID;
        public string Message;

        public MessageGM(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            WorldID = packet.ReadUInt();
            Message = packet.ReadString();
        }
    }
}
