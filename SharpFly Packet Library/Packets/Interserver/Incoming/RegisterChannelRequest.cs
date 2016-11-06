namespace SharpFly_Packet_Library.Packets.Interserver.Incoming
{
    public class RegisterChannelRequest
    {
        public string AuthorizationPassword { get; private set; }
        public string Ip { get; private set; }
        public string SendPort { get; private set; }
        public string Name { get; private set; }
        public uint MaxPlayerCount { get; private set; }

        public RegisterChannelRequest(IncomingInterserverPacket packet)
        {
            AuthorizationPassword = packet.ReadString();
            Ip = packet.ReadString();
            SendPort = packet.ReadString();
            Name = packet.ReadString();
            MaxPlayerCount = packet.ReadUInt();
        }
    }
}
