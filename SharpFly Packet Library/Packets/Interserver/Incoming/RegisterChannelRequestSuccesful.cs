namespace SharpFly_Packet_Library.Packets.Interserver.Incoming
{
    public class RegisterChannelRequestSuccesful
    {
        public bool Accepted { get; private set; }
        public uint ChannelId { get; private set; }

        public RegisterChannelRequestSuccesful(IncomingInterserverPacket packet)
        {
            Accepted = packet.ReadBool();
            ChannelId = packet.ReadUInt();
        }
    }
}
