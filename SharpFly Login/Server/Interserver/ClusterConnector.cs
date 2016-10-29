using NetMQ;
using NetMQ.Sockets;
using SharpFly_Login.Clusters;
using SharpFly_Packet_Library.Packets;
using System;

namespace SharpFly_Login.Server.Interserver
{
    class ClusterConnector
    {
        PullSocket _Server;

        public ClusterConnector()
        {
            _Server = new PullSocket("tcp://localhost:1234");
        }

        public void Dispose()
        {
            _Server.Dispose();
        }

        public void StartListening()
        {
            var poller = new NetMQPoller { _Server };
            _Server.ReceiveReady += (s, a) =>
            {
                byte[] msg;
                // Receive all messages in the poller
                while (a.Socket.TryReceiveFrameBytes(out msg))
                {
                    IncomingInterserverPacket packet = new IncomingInterserverPacket(msg);
                    Cluster.ProcessData(packet);
                }
            };

            poller.RunAsync();
        }
    }
}
