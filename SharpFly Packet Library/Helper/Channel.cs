namespace SharpFly_Packet_Library.Helper
{
    public class Channel
    {
        public uint Id { get; set; }
        public uint MaxPlayerCount { get; set; }
        public string Name { get; set; }
        public Cluster Parent { get; set; }
        public uint PlayerCount { get; set; }
    }
}
