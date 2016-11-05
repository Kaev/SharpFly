namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildMemberUninvite
    {
        public uint MasterID;
        public uint PlayerID;

        public GuildMemberUninvite(IncomingPacket packet)
        {
            MasterID = packet.ReadUInt();
            PlayerID = packet.ReadUInt();
        }
    }
}
