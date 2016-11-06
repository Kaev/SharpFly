using NetMQ;
using NetMQ.Sockets;
using SharpFly_Packet_Library.Packets;
using SharpFly_Packet_Library.Packets.Interserver.Incoming;
using System;

namespace SharpFly_Cluster.Server.Interserver
{
    public class LoginConnector : IDisposable
    {
        public delegate void RequestSuccesfulHandler(RequestSuccesfulEventArgs args);
        public event RequestSuccesfulHandler OnClusterRequestSuccesful;
        public event RequestSuccesfulHandler OnNewChannelRequestSuccesful;

        public PublisherSocket PublisherSocket { get; private set; }
        public PullSocket ServerSocket { get; private set; }

        public LoginConnector(string loginPort, string clusterStartPort)
        {
            PublisherSocket = new PublisherSocket(String.Format(">tcp://{0}:{1}", ClusterServer.Config.GetSetting("LoginAddress"), loginPort));
            ServerSocket = new PullSocket(String.Format("@tcp://{0}:{1}", ClusterServer.Config.GetSetting("Address"), clusterStartPort));
        }

        public void Dispose()
        {
            PublisherSocket.Dispose();
            ServerSocket.Dispose();
        }

        public void StartListening()
        {
            var poller = new NetMQPoller { ServerSocket };
            ServerSocket.ReceiveReady += (s, a) =>
            {
                byte[] msg;
                // Receive all messages in the poller
                while (a.Socket.TryReceiveFrameBytes(out msg))
                {
                    IncomingInterserverPacket packet = new IncomingInterserverPacket(msg);
                    ProcessData(packet);
                }
            };

            poller.RunAsync();
        }

        private void ProcessData(IncomingInterserverPacket packet)
        {
            packet.Position = 0;
            uint header = packet.ReadUInt();
            switch (header)
            {
                case OpCodes.REGISTER_CLUSTER_REQUEST_SUCCESFUL:
                    RegisterClusterRequestSuccesful(packet);
                    break;
                case OpCodes.REGISTER_NEW_CHANNEL_SUCCESFUL:
                    RegisterNewChannelSuccesful(packet);
                    break;
                default:
                    Console.WriteLine(String.Format("Unknown packet header {0}", header));
                    break;
            }
        }

        private void RegisterClusterRequestSuccesful(IncomingInterserverPacket packet)
        {
            RegisterClusterSuccesful request = new RegisterClusterSuccesful(packet);
            RequestSuccesfulEventArgs args = new RequestSuccesfulEventArgs(request.Succesful, request.ClusterId);
            OnClusterRequestSuccesful(args);
        }

        private void RegisterNewChannelSuccesful(IncomingInterserverPacket packet)
        {
            RegisterNewChannelSuccesful request = new RegisterNewChannelSuccesful(packet);
            RequestSuccesfulEventArgs args = new RequestSuccesfulEventArgs(request.Succesful, request.ChannelId, request.TempChannelId);
            OnNewChannelRequestSuccesful(args);
        }
    }
}
