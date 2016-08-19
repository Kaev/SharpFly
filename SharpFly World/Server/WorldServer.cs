using SharpFly_Packet_Library.Packets.Interserver.Outgoing;
using SharpFly_Utility_Library.Configuration;
using SharpFly_Utility_Library.Database.Databases;
using System;
using System.Net;
using System.Net.Sockets;

namespace SharpFly_World.Server
{
    class WorldServer
    {
        private Socket m_Socket { get; set; }

        public static Config Config { get; private set; }
        public static WorldDatabase WorldDatabase;

        public WorldServer(int Port)
        {
            Config = new WorldServerConfig("Resources/Config/World.ini");
            WorldDatabase = new WorldDatabase(Config);
            if (WorldDatabase.Connection.CheckConnection())
            {
                Console.WriteLine("Connecting to login server...");
                this.m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.m_Socket.Connect(IPAddress.Parse("127.0.0.1"), 1234);

                if (this.m_Socket.Connected)
                {
                    Console.WriteLine("Succesfully connected to login server!");
                    new RegisterClusterRequest((string)Config.GetSetting("ClusterAuthorizationPassword"), "SharpFly Cluster", 1, new string[] { "SharpFly" }, new uint[] { 0 }, new uint[] { 50 }, this.m_Socket);
                    Console.WriteLine("Cluster authorization request sent!");
                }
            }
        }

        public void Close()
        {
            this.m_Socket.Shutdown(SocketShutdown.Both);
            this.m_Socket.Close();
        }
    }
}
