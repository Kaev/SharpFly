namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class BountyStart
    {
        public string HuntedPlayerID;
        public uint PlayerID;
        public int Penya;
        public string Message;

        public BountyStart(IncomingPacket packet)
        {
            HuntedPlayerID = packet.ReadString();
            PlayerID = packet.ReadUInt();
            Penya = packet.ReadInt();
            Message = packet.ReadString();
        }
    }
}
