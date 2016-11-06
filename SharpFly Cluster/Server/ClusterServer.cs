using SharpFly_Utility_Library.Configuration;
using SharpFly_Utility_Library.Database.LoginDatabase;
using SharpFly_Utility_Library.Database.ClusterDatabase;
using SharpFly_Cluster.Player;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SharpFly_Cluster.Server.Interserver;
using SharpFly_Packet_Library.Packets.Interserver.Outgoing;
using SharpFly_Utility_Library.Ports;
using SharpFly_Cluster.Channel;

namespace SharpFly_Cluster.Server
{
    public class ClusterServer : IDisposable
    {
        private Socket m_PlayerSocket { get; set; }

        public static LoginConnector LoginConnector;
        public static WorldConnector WorldConnector;
        public static Config Config { get; private set; }
        public static LoginDatabase LoginDatabase;
        public static ClusterDatabase ClusterDatabase;
        public static ClientManager ClientManager;
        public static ChannelManager ChannelManager;

        public static uint ClusterId;

        public ClusterServer()
        {
            Console.WriteLine("Test if port 28000 is in use...");
            if (PortChecker.IsPortAvailable(28000))
            {
                Console.WriteLine("Port 28000 is already in use - You can only run one cluster server on one computer");
                return;
            }

            Config = new ClusterServerConfig("Resources/Config/Cluster.ini");

            int loginPort = (int)Config.GetSetting("LoginPort");
            int clusterStartPort = (int)Config.GetSetting("ClusterStartPort");

            Console.WriteLine("Searching open port for login server communication...");
            while (PortChecker.IsPortAvailable(clusterStartPort))
            {
                Console.WriteLine("Port {0} not available, let's try another port", clusterStartPort.ToString());
                clusterStartPort += 1;
            }

            LoginDatabase = new LoginDatabase(Config);
            ClusterDatabase = new ClusterDatabase(Config);
            if (ClusterDatabase.Connection.CheckConnection() && LoginDatabase.Connection.CheckConnection())
            {
                Console.WriteLine("Connecting to login server...");

                LoginConnector = new LoginConnector(loginPort.ToString(), clusterStartPort.ToString());
                LoginConnector.StartListening();

                // Let's wait a bit to let the subscriber and publisher socket to connect
                Thread.Sleep(500);
                RegisterClusterRequest request = new RegisterClusterRequest((uint)Config.GetSetting("ClusterId"), (string)Config.GetSetting("ClusterAuthorizationPassword"), (string)Config.GetSetting("Address"), clusterStartPort.ToString(), LoginConnector.PublisherSocket);

                LoginConnector.OnClusterRequestSuccesful += new LoginConnector.RequestSuccesfulHandler(OnRegisterClusterRequestSuccesful);
            }
        }

        public void Dispose()
        {
            if (ClientManager != null)
                ClientManager.Dispose();
            if (ChannelManager != null)
                ChannelManager.Dispose();
            if (LoginConnector != null)
                LoginConnector.Dispose();
            if (this.m_PlayerSocket != null)
                this.m_PlayerSocket.Dispose();
        }

        public void OnRegisterClusterRequestSuccesful(RequestSuccesfulEventArgs args)
        {
            LoginConnector.OnClusterRequestSuccesful -= OnRegisterClusterRequestSuccesful;
            if (args.Accepted)
            {
                ClusterId = args.Id;

                LoginConnector.OnNewChannelRequestSuccesful += new LoginConnector.RequestSuccesfulHandler(OnRegisterNewChannelSuccesful);

                this.m_PlayerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.m_PlayerSocket.Bind(new IPEndPoint(IPAddress.Any, 28000));
                this.m_PlayerSocket.Listen(100);

                ClientManager = new ClientManager();
                ChannelManager = new ChannelManager();
                WorldConnector = new WorldConnector(((int)Config.GetSetting("ClusterPort")).ToString());
                WorldConnector.StartListening();

                Thread acceptPlayerThread = new Thread(() => ClientManager.AcceptPlayers(this.m_PlayerSocket));
                acceptPlayerThread.Start();

                Thread processPlayerThread = new Thread(() => ClientManager.ProcessPlayers());
                processPlayerThread.Start();

                Console.WriteLine("Cluster request succesful!");
            }
            else
                Console.WriteLine("Cluster request wasn't succesful!");
        }

        public void OnRegisterNewChannelSuccesful(RequestSuccesfulEventArgs args)
        {
            Channel.Channel channel = ChannelManager.GetChannelById(args.TempId);

            if (args.Accepted)
            {

                if (channel == null)
                {
                    channel.SendRegisterChannelRequestSuccesful(false, args.Id);
                    Console.WriteLine("New channel request wasn't succesful!");
                    return;
                }

                SharpFly_Packet_Library.Helper.Cluster cluster = new SharpFly_Packet_Library.Helper.Cluster();
                cluster.Id = ClusterId;
                channel.ChannelData.Parent = cluster;
                channel.ChannelData.Id = args.Id;

                channel.SendRegisterChannelRequestSuccesful(args.Accepted, args.Id);
                Console.WriteLine("New channel request succesful!");
                return;
            }

            channel.SendRegisterChannelRequestSuccesful(false, args.Id);
            Console.WriteLine("New channel request wasn't succesful!");
        }
    }
}
