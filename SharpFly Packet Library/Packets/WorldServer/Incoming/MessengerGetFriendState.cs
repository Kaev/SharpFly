namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MessengerGetFriendState
    {
        public uint PlayerID;

        public MessengerGetFriendState(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
        }
    }
}
