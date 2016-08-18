namespace SharpFly_Login.Clusters
{
    public class Channel
    {
        public Cluster Parent { get; set; }
        public uint Id { get; set; }
        public string Name { get; set; }
        public uint PlayerCount { get; set; }
        public uint MaxPlayerCount { get; set; }
    }
}
