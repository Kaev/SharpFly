namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PartyMemberUninvite
    {
        public uint LeaderID;
        public uint MemberID;

        PartyMemberUninvite(IncomingPacket packet)
        {
            LeaderID = packet.ReadUInt();
            MemberID = packet.ReadUInt();
        }
    }
}
