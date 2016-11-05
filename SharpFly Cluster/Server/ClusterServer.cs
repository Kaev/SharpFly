using SharpFly_Packet_Library.Security;
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

namespace SharpFly_Cluster.Server
{
    public class ClusterServer : IDisposable
    {
        private Socket m_PlayerSocket { get; set; }

        public static LoginConnector LoginConnector;
        public static Config Config { get; private set; }
        public static LoginDatabase LoginDatabase;
        public static ClientManager PlayerManager;
        public static ClusterDatabase ClusterDatabase;

        public ClusterServer()
        {
            Console.WriteLine("Test if port 28000 is in use...");
            if (PortChecker.IsPortAvailable(28000))
            {
                Console.WriteLine("Port 28000 is already in use - You can only run one cluster server on one computer");
                return;
            }

            Rijndael.Initiate();

            Config = new ClusterServerConfig("Resources/Config/Cluster.ini");

            int loginPort = (int)Config.GetSetting("LoginPort");

            int receivePort = (int)Config.GetSetting("InterserverPort");
            Console.WriteLine("Search open port for interserver connection...");
            while (PortChecker.IsPortAvailable(receivePort))
            {
                Console.WriteLine("Port {0} not available, let's try another port", receivePort.ToString());
                receivePort += 1;
            }

            LoginDatabase = new LoginDatabase(Config);
            ClusterDatabase = new ClusterDatabase(Config);
            if (ClusterDatabase.Connection.CheckConnection() && LoginDatabase.Connection.CheckConnection())
            {
                Console.WriteLine("Connecting to login server...");

                LoginConnector = new LoginConnector(loginPort.ToString(), receivePort.ToString());
                LoginConnector.StartListening();

                // Let's wait a bit to let the subscriber and publisher socket to connect
                Thread.Sleep(500);
                RegisterClusterRequest request = new RegisterClusterRequest((uint)Config.GetSetting("ClusterId"), (string)Config.GetSetting("ClusterAuthorizationPassword"), (string)Config.GetSetting("ClusterAddress"), receivePort.ToString(), LoginConnector.PublisherSocket);

                LoginConnector.OnClusterRequestSuccesful += new LoginConnector.RequestSuccesfulHandler(OnRegisterClusterRequestSuccesful);
            }
        }

        public void Dispose()
        {
            if (PlayerManager != null)
                PlayerManager.Dispose();
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
                RegisterNewChannelRequest newChannelRequest = new RegisterNewChannelRequest(args.Id, (string)Config.GetSetting("ClusterAuthorizationPassword"), "SharpFly Channel", 0, 50, LoginConnector.PublisherSocket);
                LoginConnector.OnNewChannelRequestSuccesful += new LoginConnector.RequestSuccesfulHandler(OnRegisterNewChannelSuccesful);

                this.m_PlayerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.m_PlayerSocket.Bind(new IPEndPoint(IPAddress.Any, 28000));
                this.m_PlayerSocket.Listen(100);

                PlayerManager = new ClientManager();

                Thread acceptPlayerThread = new Thread(() => PlayerManager.AcceptPlayers(this.m_PlayerSocket));
                acceptPlayerThread.Start();

                Thread processPlayerThread = new Thread(() => PlayerManager.ProcessPlayers());
                processPlayerThread.Start();

                Console.WriteLine("Cluster request succesful!");
            }
            else
                Console.WriteLine("Cluster request wasn't succesful!");
        }

        public void OnRegisterNewChannelSuccesful(RequestSuccesfulEventArgs args)
        {
            if (args.Accepted)
            {
                Console.WriteLine("New channel request succesful!");
            }
        }
    }
}
