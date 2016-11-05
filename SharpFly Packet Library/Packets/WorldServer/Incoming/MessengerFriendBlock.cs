namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MessengerFriendBlock
    {
        public uint PlayerID;
        public uint BlockedPlayerID;

        public MessengerFriendBlock(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            BlockedPlayerID = packet.ReadUInt();
        }
    }
}
