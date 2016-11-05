namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class RechargeIDStack
    {
        public uint BlockSize;      //No idea.

        RechargeIDStack(IncomingPacket packet)
        {
            BlockSize = packet.ReadUInt();
        }
    }
}
