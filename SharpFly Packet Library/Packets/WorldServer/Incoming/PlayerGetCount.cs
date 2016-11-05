namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PlayerGetCount
    {
        public uint RequestingPlayerID;

        public PlayerGetCount(IncomingPacket packet)
        {
            RequestingPlayerID = packet.ReadUInt();
        }
    }
}
