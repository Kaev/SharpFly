namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildSendTruceRequest
    {
        public uint PlayerID;

        public GuildSendTruceRequest(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
        }
    }
}
