namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildWarDeclare
    {
        public uint MasterID;
        public string GuildName;

        public GuildWarDeclare(IncomingPacket packet)
        {
            MasterID = packet.ReadUInt();
            GuildName = packet.ReadString();
        }
    }
}
