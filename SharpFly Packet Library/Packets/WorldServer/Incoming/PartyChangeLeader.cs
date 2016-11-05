namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PartyChangeLeader
    {
        public uint LeaderID;
        public uint NewLeaderID;

        public PartyChangeLeader(IncomingPacket packet)
        {
            LeaderID = packet.ReadUInt();
            NewLeaderID = packet.ReadUInt();
        }
    }
}
