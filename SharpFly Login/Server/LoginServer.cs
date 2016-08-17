using System;
using System.Net.Sockets;
using SharpFly_Login.Clients;
using System.Net;
using System.Threading;
using SharpFly_Utility_Library.Database.Databases;
using SharpFly_Utility_Library.Configuration;

namespace SharpFly_Login.Server
{
    class LoginServer
    {
        private int m_Port { get; set; }
        private Socket m_Socket { get; set; }

        public static Config Config { get; private set; }
        public static ClientManager ClientManager;
        public static LoginDatabase LoginDatabase;

        public LoginServer(int Port)
        {
            Config = new LoginServerConfig("Resources/Config/Login.ini");
            LoginDatabase = new LoginDatabase(Config);
            if (LoginDatabase.Connection.CheckConnection())
            {
                Security.Rijndael.Initiate();
                this.m_Port = Port;
                this.m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.m_Socket.Bind(new IPEndPoint(IPAddress.Any, this.m_Port));
                this.m_Socket.Listen(100);
                ClientManager = new ClientManager();

                Console.WriteLine("Login server started");

                Thread acceptClientsThread = new Thread(() => ClientManager.AcceptUsers(this.m_Socket));
                acceptClientsThread.Start();

                Thread processClientsThread = new Thread(() => ClientManager.ProcessUsers());
                processClientsThread.Start();
            }
        }

        public void Close()
        {
            this.m_Socket.Shutdown(SocketShutdown.Both);
            this.m_Socket.Close();
        }
    }
}
