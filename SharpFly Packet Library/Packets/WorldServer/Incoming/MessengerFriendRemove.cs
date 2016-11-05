namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MessengerFriendRemove
    {
        public uint PlayerID;
        public uint RemovedFriendID;

        public MessengerFriendRemove(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            RemovedFriendID = packet.ReadUInt();
        }
    }
}
