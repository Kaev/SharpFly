namespace SharpFly_Packet_Library.Packets.Interserver.Incoming
{
    public class UpdateClusterChannelPlayerCount
    {
        public uint ChannelId { get; private set; }
        public uint NewPlayerCount { get; private set; }

        public UpdateClusterChannelPlayerCount(IncomingPacket packet)
        {
            ChannelId = packet.ReadUInt();
            NewPlayerCount = packet.ReadUInt();
        }
    }
}
