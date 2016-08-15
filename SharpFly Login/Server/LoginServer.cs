using System;
using System.Net.Sockets;
using SharpFly_Login.Clients;
using System.Net;
using System.Threading;
using SharpFly_Utility_Library.Database.Databases;
using SharpFly_Utility_Library.Configuration;
using SharpFly_Utility_Library.Database;

namespace SharpFly_Login.Server
{
    class LoginServer
    {
        public int Port { get; private set; }
        private Socket Socket { get; set; }

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
                this.Port = Port;
                this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.Socket.Bind(new IPEndPoint(IPAddress.Any, this.Port));
                this.Socket.Listen(100);
                ClientManager = new ClientManager();

                Console.WriteLine("Login server started");

                Thread acceptClientsThread = new Thread(() => ClientManager.AcceptUsers(this.Socket));
                acceptClientsThread.Start();

                Thread processClientsThread = new Thread(() => ClientManager.ProcessUsers());
                processClientsThread.Start();
            }
        }

        public void Close()
        {
            this.Socket.Shutdown(SocketShutdown.Both);
            this.Socket.Close();
        }
    }
}
