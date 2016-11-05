namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PlayerGetCoreCount
    {
        public uint OperatorID;

        public PlayerGetCoreCount(IncomingPacket packet)
        {
            OperatorID = packet.ReadUInt();
        }
    }
}
