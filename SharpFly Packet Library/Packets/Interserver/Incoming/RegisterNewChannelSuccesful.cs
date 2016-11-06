namespace SharpFly_Packet_Library.Packets.Interserver.Incoming
{
    public class RegisterNewChannelSuccesful
    {
        public uint ChannelId { get; private set; }
        public uint TempChannelId { get; private set; }
        public bool Succesful { get; private set; }

        public RegisterNewChannelSuccesful(IncomingInterserverPacket packet)
        {
            ChannelId = packet.ReadUInt();
            TempChannelId = packet.ReadUInt();
            Succesful = packet.ReadBool();
        }
    }
}
