using System.Collections.Generic;

namespace SharpFly_Packet_Library.Helper
{
    public class Cluster
    {
        public int ParentId { get; private set; } = -1;
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public int Is18Plus { get; private set; } = 0;
        public uint PlayerCount { get; private set; } = 0;
        public int Enabled { get; set; } = 1;
        public uint MaxPlayerCount { get; private set; } = 0;
        public List<Channel> Channels { get; set; }

        public Cluster()
        {
            Channels = new List<Channel>();
        }
    }
}
