namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PartyLevel
    {
        public uint PartyID;
        public uint PlayerID;
        public uint Level;
        public uint Points;
        public uint Exp;

        public PartyLevel(IncomingPacket packet)
        {
            PartyID = packet.ReadUInt();
            PlayerID = packet.ReadUInt();
            Level = packet.ReadUInt();
            Points = packet.ReadUInt();
            Exp = packet.ReadUInt();
        }
    }
}
