namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MessageParty
    {
        public uint ObjID;      //Probably the HexFont item?
        public uint PlayerID;
        public string Message;

        public MessageParty(IncomingPacket packet)
        {
            ObjID = packet.ReadUInt();
            PlayerID = packet.ReadUInt();
            Message = packet.ReadString();
        }
    }
}
