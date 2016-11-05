namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PartySkillUse
    {
        public uint PlayerID;
        public int Mode;                //?
        public uint SkillTime;
        public int PointsRemoved;
        public int CacheMode;           //?

        public PartySkillUse(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            Mode = packet.ReadInt();
            SkillTime = packet.ReadUInt();
            PointsRemoved = packet.ReadInt();
            CacheMode = packet.ReadInt();
        }
    }
}
