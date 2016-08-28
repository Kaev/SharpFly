using System;
using SharpFly_Cluster.Server;

namespace SharpFly_Cluster
{
    class Program
    {
        public static ClusterServer ClusterServer;

        static void Main(string[] args)
        {
            ClusterServer = new ClusterServer(28000);
            Console.ReadLine();
        }
    }
}
