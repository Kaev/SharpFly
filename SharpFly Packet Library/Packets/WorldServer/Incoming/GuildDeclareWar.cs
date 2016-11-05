namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildDeclareWar
    {
        public uint MasterID;
        public string GuildName;

        public GuildDeclareWar(IncomingPacket packet)
        {
            MasterID = packet.ReadUInt();
            GuildName = packet.ReadString();
        }
    }
}
