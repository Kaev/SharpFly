namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildCombatCreateParty
    {
        public uint LeaderID;
        public int LeaderLevel;
        public int LeaderJob;
        public uint LeaderSex;
        public uint MemberID;
        public int MemberLevel;
        public int MemberJob;
        public uint MemberSex;

        public GuildCombatCreateParty(IncomingPacket packet)
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
