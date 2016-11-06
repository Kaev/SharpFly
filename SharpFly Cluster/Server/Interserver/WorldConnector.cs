using NetMQ;
using NetMQ.Sockets;
using SharpFly_Packet_Library.Packets;
using System;
using System.Collections;

namespace SharpFly_Cluster.Server.Interserver
{
    public class WorldConnector : IDisposable
    {
        SubscriberSocket _SubscriberSocket { get; set; }

        public WorldConnector(string worldRegisterPort)
        {
            _SubscriberSocket = new SubscriberSocket(String.Format("@tcp://{0}:{1}", ClusterServer.Config.GetSetting("Address"), worldRegisterPort));
            _SubscriberSocket.Subscribe("SharpFlyCluster");
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
                    if (StructuralComparisons.StructuralEqualityComparer.Equals(msg, System.Text.Encoding.UTF8.GetBytes("SharpFlyCluster")))
                        return;
                    IncomingInterserverPacket packet = new IncomingInterserverPacket(msg);
                    Channel.Channel.ProcessData(packet);
                }
            };

            poller.RunAsync();
        }
    }
}
