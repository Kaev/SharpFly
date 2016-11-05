namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PartyRequestDuel
    {
        public uint PartyID1;
        public uint PartyID2;
        public bool Duel;

        public PartyRequestDuel(IncomingPacket packet)
        {
            PartyID1 = packet.ReadUInt();
            PartyID2 = packet.ReadUInt();
            Duel = packet.ReadBool();
        }
    }
}
