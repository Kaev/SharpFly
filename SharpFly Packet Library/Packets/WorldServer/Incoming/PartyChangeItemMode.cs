namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PartyChangeItemMode
    {
        public uint PlayerID;
        public int ItemMode;

        public PartyChangeItemMode(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            ItemMode = packet.ReadInt();
        }
    }
}
