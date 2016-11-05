namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildMemberSetNickName
    {
        public uint MasterID;
        public uint PlayerID;
        public string NickName;

        public GuildMemberSetNickName(IncomingPacket packet)
        {
            MasterID = packet.ReadUInt();
            PlayerID = packet.ReadUInt();
            NickName = packet.ReadString();
        }
    }
}
