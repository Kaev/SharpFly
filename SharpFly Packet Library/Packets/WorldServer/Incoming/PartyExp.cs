namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PartyExp
    {
        public uint PartyID;
        public int Level; //PartyLevel?
        public bool AdvancedPartyScroll;
        public bool ScrollOfPartyEXP;

        public PartyExp(IncomingPacket packet)
        {
            PartyID = packet.ReadUInt();
            Level = packet.ReadInt();
            AdvancedPartyScroll = packet.ReadBool();
            ScrollOfPartyEXP = packet.ReadBool();
        }
    }
}
