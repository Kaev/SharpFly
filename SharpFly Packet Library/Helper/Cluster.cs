namespace SharpFly_Packet_Library.Helper
{
    public class Cluster
    {
        public Channel[] Channel { get; set; }
        public uint ChannelCount { get; set; }
        public uint Id { get; set; }
        public string Ip { get; set; }
        public string Name { get; set; }
    }
}
