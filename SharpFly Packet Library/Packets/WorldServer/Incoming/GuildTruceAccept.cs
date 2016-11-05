namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildTruceAccept
    {
        public uint PlayerID;

        public GuildTruceAccept(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
        }
    }
}
