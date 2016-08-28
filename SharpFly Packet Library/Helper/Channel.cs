namespace SharpFly_Packet_Library.Helper
{
    public class Channel
    {
        public Cluster Parent { get; set; }
        public uint Id { get; set; }
        public string Name { get; set; }
        public int Ip { get; private set; } = 0;
        public int Is18Plus { get; private set; } = 0;
        public uint PlayerCount { get; set; }
        public int Enabled { get; set; } = 1;
        public uint MaxPlayerCount { get; set; }
    }
}
