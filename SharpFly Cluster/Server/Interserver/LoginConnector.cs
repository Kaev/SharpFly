using NetMQ;
using NetMQ.Sockets;
using System;

namespace SharpFly_Cluster.Server.Interserver
{
    public class LoginConnector : IDisposable
    {
        public PushSocket Socket { get; private set; }

        public LoginConnector()
        {
            Socket = new PushSocket("tcp://localhost:1234");
        }

        public void Dispose()
        {
            Socket.Dispose();
        }
    }
}
