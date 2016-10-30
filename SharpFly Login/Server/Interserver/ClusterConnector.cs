using NetMQ;
using NetMQ.Sockets;
using SharpFly_Login.Clusters;
using SharpFly_Packet_Library.Packets;
using System;
using System.Collections;

namespace SharpFly_Login.Server.Interserver
{
    class ClusterConnector
    {
        SubscriberSocket _SubscriberSocket { get; set; }

        public ClusterConnector()
        {
            _SubscriberSocket = new SubscriberSocket(String.Format(">tcp://{0}:1234", LoginServer.Config.GetSetting("LoginAddress")));
            _SubscriberSocket.Subscribe("SharpFlyLogin");
        }

        public void Dispose()
        {
            _SubscriberSocket.Dispose();
        }

        public void StartListening()
        {
            var poller = new NetMQPoller { _SubscriberSocket };
            _SubscriberSocket.ReceiveReady += (s, a) =>
            {
                byte[] msg;
                // Receive all messages in the poller
                while (a.Socket.TryReceiveFrameBytes(out msg))
                {
                    // Ignore the publisher-subscriber message before the real packet
                    if (StructuralComparisons.StructuralEqualityComparer.Equals(msg, System.Text.Encoding.UTF8.GetBytes("SharpFlyLogin")))
                        return;
                    IncomingInterserverPacket packet = new IncomingInterserverPacket(msg);
                    Cluster.ProcessData(packet);
                }
            };

            poller.RunAsync();
        }
    }
}
