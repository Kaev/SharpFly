namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class SnoopSet
    {
        public uint PlayerID;
        public uint Snoop;
        public bool IfSnoop;

        public SnoopSet(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            Snoop = packet.ReadUInt();
            IfSnoop = packet.ReadBool();
        }
    }
}
