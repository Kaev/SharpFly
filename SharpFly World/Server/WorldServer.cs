using SharpFly_Packet_Library.Packets.Interserver.Outgoing;
using SharpFly_Utility_Library.Configuration;
using SharpFly_Utility_Library.Database.WorldDatabase;
using SharpFly_Utility_Library.Ports;
using SharpFly_World.Server.Interserver;
using System;
using System.Threading;

namespace SharpFly_World.Server
{
    public class WorldServer : IDisposable
    {

        public static Config Config { get; private set; }
        public static ClusterConnector ClusterConnector;
        public static WorldDatabase WorldDatabase;

        public WorldServer()
        {
            Config = new WorldServerConfig("Resources/Config/World.ini");

            WorldDatabase = new WorldDatabase(Config);
            if (WorldDatabase.Connection.CheckConnection())
            {
                Console.WriteLine("Connecting to cluster server...");

                int clusterPort = (int)Config.GetSetting("ClusterPort");
                int worldStartPort = (int)Config.GetSetting("WorldStartPort");

                Console.WriteLine("Searching open port for cluster server communication...");
                while (PortChecker.IsPortAvailable(worldStartPort))
                {
                    Console.WriteLine("Port {0} not available, let's try another port", worldStartPort.ToString());
                    worldStartPort += 1;
                }

                ClusterConnector = new ClusterConnector(clusterPort.ToString(), worldStartPort.ToString());
                ClusterConnector.StartListening();

                // Let's wait a bit to let the subscriber and publisher socket to connect
                Thread.Sleep(1500);
                RegisterChannelRequest request = new RegisterChannelRequest((string)Config.GetSetting("ClusterAuthorizationPassword"), (string)Config.GetSetting("Address"), worldStartPort.ToString(), (string)Config.GetSetting("ChannelName"), 50, ClusterConnector.PublisherSocket);

                ClusterConnector.OnRegisterChannelSuccesful += new ClusterConnector.RequestSuccesfulHandler(OnRegisterChannelSuccesful);
            }
        }

        public void Dispose()
        {
            if (ClusterConnector != null)
                ClusterConnector.Dispose();
        }

        public void OnRegisterChannelSuccesful(RequestSuccesfulEventArgs args)
        {
            if (args.Accepted)
                Console.WriteLine("Channel {0} was registered with Id {1}!", (string)Config.GetSetting("ChannelName"), args.Id);
            else
                Console.WriteLine("Channel wasn't registered!");
        }
    }
}
