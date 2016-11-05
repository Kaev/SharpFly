namespace SharpFly_Packet_Library.Packets.WorldServer
{
    public class LeavePlayer
    {
        public uint PlayerID;

        public LeavePlayer(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
        }
    }
}
