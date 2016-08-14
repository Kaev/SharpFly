using System;
using System.Net.Sockets;
using SharpFly_Login.Clients;
using SharpFly_Login.Database;
using System.Net;
using System.Threading;
using SharpFly_Utility_Library;

namespace SharpFly_Login.Server
{
    class LoginServer
    {
        public bool ConfigLoaded { get; private set; }
        public int Port { get; private set; }
        public Socket Socket { get; private set; }

        public static ClientManager ClientManager;

        public LoginServer(int Port)
        {
            ConfigLoaded = false;
            if (Config.ReadConfig() && MySQL.CheckConnection()) // Read config file and check Mysql connection
            {
                this.ConfigLoaded = true;
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
