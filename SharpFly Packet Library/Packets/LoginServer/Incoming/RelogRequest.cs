namespace SharpFly_Packet_Library.Packets.LoginServer.Incoming
{
    public class RelogRequest
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public RelogRequest(IncomingPacket packet)
        {
            Username = packet.ReadString();
            Password = packet.ReadString();
        }
    }
}
