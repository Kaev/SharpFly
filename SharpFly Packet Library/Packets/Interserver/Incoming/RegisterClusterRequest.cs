namespace SharpFly_Packet_Library.Packets.Interserver.Incoming
{
    public class RegisterClusterRequest
    {
        public uint ClusterId { get; private set; }
        public string AuthorizationPassword { get; private set; }
        public string Name { get; private set; }
        public string Ip { get; private set; }
        public string SendPort { get; private set; }

        public RegisterClusterRequest(IncomingInterserverPacket packet)
        {
            ClusterId = packet.ReadUInt();
            AuthorizationPassword = packet.ReadString();
            Name = packet.ReadString();
            Ip = packet.ReadString();
            SendPort = packet.ReadString();
        }
    }
}
