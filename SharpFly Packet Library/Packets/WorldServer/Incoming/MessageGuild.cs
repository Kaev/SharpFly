namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MessageGuild
    {
        public uint ObjectID;           //?
        public uint PlayerID;
        public string Message;

        public MessageGuild(IncomingPacket packet)
        {
            ObjectID = packet.ReadUInt();
            PlayerID = packet.ReadUInt();
            Message = packet.ReadString();
        }
    }
}
