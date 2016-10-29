using System;
using System.Net.Sockets;
using SharpFly_Login.Clients;
using System.Net;
using System.Threading;
using SharpFly_Packet_Library.Security;
using SharpFly_Utility_Library.Configuration;
using SharpFly_Login.Clusters;
using SharpFly_Utility_Library.Database.LoginDatabase;
using SharpFly_Login.Server.Interserver;

namespace SharpFly_Login.Server
{
    public class LoginServer : IDisposable
    {
        private Socket m_ClientSocket { get; set; }
        private ClusterConnector m_ClusterConnector { get; set; }

        public static Config Config { get; private set; }
        public static ClientManager ClientManager;
        public static ClusterManager ClusterManager;
        public static LoginDatabase LoginDatabase;

        public LoginServer(int Port)
        {
            Config = new LoginServerConfig("Resources/Config/Login.ini");
            LoginDatabase = new LoginDatabase(Config);
            if (LoginDatabase.Connection.CheckConnection())
            {
                Rijndael.Initiate();

                this.m_ClusterConnector = new ClusterConnector();
                this.m_ClusterConnector.StartListening();
                ClusterManager = new ClusterManager();
                Console.WriteLine("Listening for cluster servers...");

                this.m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.m_ClientSocket.Bind(new IPEndPoint(IPAddress.Any, Port));
                this.m_ClientSocket.Listen(100);
                ClientManager = new ClientManager();

                Thread acceptClientsThread = new Thread(() => ClientManager.AcceptUsers(this.m_ClientSocket));
                acceptClientsThread.Start();

                Thread processClientsThread = new Thread(() => ClientManager.ProcessUsers());
                processClientsThread.Start();

                Console.WriteLine("Listening for clients...");
            }
        }

        public void Dispose()
        {
            this.m_ClusterConnector.Dispose();
            this.m_ClientSocket.Shutdown(SocketShutdown.Both);
            this.m_ClientSocket.Close();
        }
    }
}
