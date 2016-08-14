namespace SharpFly_Packet_Library.Packets.LoginServer.Incoming
{
    public class LoginRequest
    {
        public string BuildDate { get; private set; }
        public string Username { get; private set; }
        public byte[] Password { get; private set; }

        public LoginRequest(IncomingPacket packet)
        {
            BuildDate = packet.ReadString();
            Username = packet.ReadString();
            Password = packet.ReadBytes(16 * 42); // PasswordSize
        }
    }
}
