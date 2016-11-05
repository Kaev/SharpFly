namespace SharpFly_Packet_Library.Packets.WorldServer
{
    public class PreJoin
    {
        public uint AuthKey;
        public string AccountName;
        public uint PlayerID;
        public string PlayerName;

        public PreJoin(IncomingPacket packet)
        {
            AuthKey = packet.ReadUInt();
            AccountName = packet.ReadString();
            PlayerID = packet.ReadUInt();
            PlayerName = packet.ReadString();
        }
    }
}
