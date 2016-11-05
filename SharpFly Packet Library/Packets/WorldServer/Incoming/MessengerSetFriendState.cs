namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MessengerSetFriendState
    {
        public uint PlayerID;
        public int PlayerState;

        public MessengerSetFriendState(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            PlayerState = packet.ReadInt();
        }
    }
}
