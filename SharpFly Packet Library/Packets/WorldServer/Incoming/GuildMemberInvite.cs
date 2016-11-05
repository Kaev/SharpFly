namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildMemberInvite
    {
        public uint MasterID;
        /*public TODO: GUILD_MEMBER_INFO = PlayerID;*/

        public GuildMemberInvite(IncomingPacket packet)
        {
            MasterID = packet.ReadUInt();
            //packet.Read( &info, sizeof(GUILD_MEMBER_INFO) ); TODO
        }

    }
}
