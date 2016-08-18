namespace SharpFly_Packet_Library.Packets.Interserver.Incoming
{
    public class RegisterClusterRequest
    {
        public string ClusterAuthorizationPassword { get; private set; }
        public string ClusterName { get; private set; }
        public uint ChannelCount { get; private set; }
        public string[] ChannelName { get; private set; }
        public uint[] ChannelPlayerCount { get; private set; }
        public uint[] ChannelMaxPlaxerCount { get; private set; }

        public RegisterClusterRequest(IncomingPacket packet)
        {
            ClusterAuthorizationPassword = packet.ReadString();
            ClusterName = packet.ReadString();
            ChannelCount = packet.ReadUInt();
            ChannelName = new string[ChannelCount];
            for (int i = 0; i < ChannelCount; i++)
                ChannelName[i] = packet.ReadString();
            ChannelPlayerCount = new uint[ChannelCount];
            for (int i = 0; i < ChannelCount; i++)
                ChannelPlayerCount[i] = packet.ReadUInt();
            ChannelMaxPlaxerCount = new uint[ChannelCount];
            for (int i = 0; i < ChannelCount; i++)
                ChannelMaxPlaxerCount[i] = packet.ReadUInt();
        }
    }
}
