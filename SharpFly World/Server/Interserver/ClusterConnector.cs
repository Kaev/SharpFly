using NetMQ;
using NetMQ.Sockets;
using SharpFly_Packet_Library.Packets;
using SharpFly_Packet_Library.Packets.Interserver.Incoming;
using System;

namespace SharpFly_World.Server.Interserver
{
    public class ClusterConnector : IDisposable
    {
        public delegate void RequestSuccesfulHandler(RequestSuccesfulEventArgs args);
        public event RequestSuccesfulHandler OnRegisterChannelSuccesful;

        public PublisherSocket PublisherSocket { get; private set; }
        public PullSocket ServerSocket { get; set; }

        public ClusterConnector(string clusterPort, string worldStartPort)
        {
            PublisherSocket = new PublisherSocket(String.Format(">tcp://{0}:{1}", WorldServer.Config.GetSetting("Address"), clusterPort));
            ServerSocket = new PullSocket(String.Format("@tcp://{0}:{1}", WorldServer.Config.GetSetting("Address"), worldStartPort));
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
                case OpCodes.REGISTER_CHANNEL_REQUEST_SUCCESFUL:
                    RegisterChannelRequestSuccesful(packet);
                    break;
                default:
                    Console.WriteLine(String.Format("Unknown packet header {0}", header));
                    break;
            }
        }

        private void RegisterChannelRequestSuccesful(IncomingInterserverPacket packet)
        {
            RegisterChannelRequestSuccesful request = new RegisterChannelRequestSuccesful(packet);
            RequestSuccesfulEventArgs args = new RequestSuccesfulEventArgs(request.Accepted, request.ChannelId);
            OnRegisterChannelSuccesful(args);
        }
    }
}
