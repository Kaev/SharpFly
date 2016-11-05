namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MessengerBlockFriend
    {
        public uint PlayerID;
        public uint BlockedPlayerID;

        public MessengerBlockFriend(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            BlockedPlayerID = packet.ReadUInt();
        }
    }
}
