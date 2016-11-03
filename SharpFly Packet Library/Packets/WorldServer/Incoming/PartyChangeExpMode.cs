namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PartyChangeExpMode
    {
        public uint PlayerID;
        public int ExpMode;

        public PartyChangeExpMode(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            ExpMode = packet.ReadInt();
        }
    }
}
