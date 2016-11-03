namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PartyMemberInvite
    {
        //Party leader information
        public uint LeaderID;           
        public int LeaderLevel;
        public int LeaderJob;
        public uint LeaderSex;

        //Party member information
        public uint MemberID;
        public int MemberLevel;
        public int MemberJob;
        public uint MemberSex;

        public PartyMemberInvite(IncomingPacket packet)
        {
            LeaderID = packet.ReadUInt();
            LeaderLevel = packet.ReadInt();
            LeaderJob = packet.ReadInt();
            LeaderSex = packet.ReadUInt();

            MemberID = packet.ReadUInt();
            MemberLevel = packet.ReadInt();
            MemberJob = packet.ReadInt();
            MemberSex = packet.ReadUInt();
        }
    }
}
