namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MessageCaption
    {
        public bool Small;          //Has no function
        public uint WorldID;
        public string Message;

        public MessageCaption(IncomingPacket packet)
        {
            Small = packet.ReadBool();
            WorldID = packet.ReadUInt();
            Message = packet.ReadString();
        }
    }
}
