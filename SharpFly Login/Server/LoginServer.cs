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
using SharpFly_Utility_Library.Ports;

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

        public LoginServer()
        {
            Console.WriteLine("Test if port 23000 is in use...");
            if (PortChecker.IsPortAvailable(23000))
            {
                Console.WriteLine("Port 23000 is already in use - You can only run one login server on one computer");
                return;
            }

            Config = new LoginServerConfig("Resources/Config/Login.ini");

            int loginPort = (int)Config.GetSetting("LoginPort");

            LoginDatabase = new LoginDatabase(Config);
            if (LoginDatabase.Connection.CheckConnection())
            {
                Rijndael.Initiate();

                this.m_ClusterConnector = new ClusterConnector(loginPort.ToString());
                this.m_ClusterConnector.StartListening();

                ClusterManager = new ClusterManager();
                Console.WriteLine("Listening for cluster servers...");

                this.m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.m_ClientSocket.Bind(new IPEndPoint(IPAddress.Any, 23000));
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
            if (ClientManager != null)
                ClientManager.Dispose();
            if (ClusterManager != null)
                ClusterManager.Dispose();
            if (this.m_ClusterConnector != null)
                this.m_ClusterConnector.Dispose();
            if (this.m_ClientSocket != null)
                this.m_ClientSocket.Dispose();
        }
    }
}
