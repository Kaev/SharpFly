namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class CharacterListRequest
    {
        public string BuildDate { get; private set; }
        public string Username { get; private set; }
        //public byte[] Password { get; private set; }
        public string Password { get; private set; }
        public uint ServerId { get; private set; }
        public uint TimeGetTime { get; private set; }

        public CharacterListRequest(IncomingPacket packet)
        {
            BuildDate = packet.ReadString();
            TimeGetTime = packet.ReadUInt();
            Username = packet.ReadString();
            //Password = packet.ReadBytes(16 * 42); // PasswordSize
            Password = packet.ReadString();
            ServerId = packet.ReadUInt();
        }
    }
}
