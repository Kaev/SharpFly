using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SharpFly_Login.Clients;
using SharpFly_Login.Utility;
using SharpFly_Login.Database;
using System.Net;
using System.Threading;

namespace SharpFly_Login.Server
{
    class LoginServer
    {
        public bool ConfigLoaded { get; set; }
        public int Port { get; set; }
        public Socket Socket { get; set; }

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
                Console.WriteLine("Loginserver started");

                ClientManager = new ClientManager();

                Thread acceptThread = new Thread(() => ClientManager.AcceptUsers(this.Socket));
                acceptThread.Start();

                Thread processThread = new Thread(() => ClientManager.ProcessUsers());
                processThread.Start();
            }
        }
    }
}
