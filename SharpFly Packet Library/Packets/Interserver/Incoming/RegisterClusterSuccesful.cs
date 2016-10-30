namespace SharpFly_Packet_Library.Packets.Interserver.Incoming
{
    public class RegisterClusterSuccesful
    {
        public uint ClusterId { get; private set; }
        public bool Succesful { get; private set; }

        public RegisterClusterSuccesful(IncomingInterserverPacket packet)
        {
            ClusterId = packet.ReadUInt();
            Succesful = packet.ReadBool();
        }
    }
}
