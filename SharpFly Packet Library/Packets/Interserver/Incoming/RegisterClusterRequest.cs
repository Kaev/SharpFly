namespace SharpFly_Packet_Library.Packets.Interserver.Incoming
{
    public class RegisterClusterRequest
    {
        public string AuthorizationPassword { get; private set; }
        public uint Id { get; private set; }
        public string Name { get; private set; }
        public string Ip { get; private set; }

        public RegisterClusterRequest(IncomingInterserverPacket packet)
        {
            AuthorizationPassword = packet.ReadString();
            Id = packet.ReadUInt();
            Name = packet.ReadString();
            Ip = packet.ReadString();
        }
    }
}
