namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MonsterRespawn
    {
        public uint KillerPlayerID;
        public uint MonsterID;
        public uint RespawnNumber;          //Amount of respawning monsters?
        public uint AttackNum;              //No idea. Maybe chance for aggro mobs?
        public uint Rect;                   //Maybe a (radius) of an area?
        public uint RespawnTime;
        public bool IsFlying;

        public MonsterRespawn(IncomingPacket packet)
        {
            KillerPlayerID = packet.ReadUInt();
            MonsterID = packet.ReadUInt();
            RespawnNumber = packet.ReadUInt();
            AttackNum = packet.ReadUInt();
            Rect = packet.ReadUInt();
            RespawnTime = packet.ReadUInt();
            IsFlying = packet.ReadBool();
        }
    }
}
