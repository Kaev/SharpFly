namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildWarMasterAbsent
    {
        public uint WarID;
        public bool Reaction;           //Did GuildLeader Accept or Decline?

        public GuildWarMasterAbsent(IncomingPacket packet)
        {
            WarID = packet.ReadUInt();
            Reaction = packet.ReadBool();
        }
    }
}
