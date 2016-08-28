using SharpFly_Packet_Library.Packets.Interserver.Outgoing;
using System;
using System.Net;
using System.Net.Sockets;

namespace SharpFly_Cluster.Server
{
    public class LoginServerConnector : IDisposable
    {
        private Socket m_Socket;

        public LoginServerConnector()
        {
            m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Dispose()
        {
            this.m_Socket.Shutdown(SocketShutdown.Both);
            this.m_Socket.Close();
        }

        public bool TryConnectToLoginServer()
        {
            this.m_Socket.Connect(IPAddress.Parse("127.0.0.1"), 1234);

            if (this.m_Socket.Connected)
            {
                Console.WriteLine("Succesfully connected to login server!");
                //new RegisterClusterRequest((string)ClusterServer.Config.GetSetting("ClusterAuthorizationPassword"), "SharpFly Cluster", 1, new string[] { "SharpFly" }, new uint[] { 0 }, new uint[] { 50 }, this.m_Socket);
                Console.WriteLine("Cluster authorization request sent!");

                //TODO: Add check if connection was succesful

                return true;
            }
            return false;
        }
    }
}
