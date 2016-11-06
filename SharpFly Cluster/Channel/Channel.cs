using NetMQ.Sockets;
using SharpFly_Cluster.Server;
using SharpFly_Packet_Library.Packets;
using SharpFly_Packet_Library.Packets.Interserver.Incoming;
using System;

namespace SharpFly_Cluster.Channel
{
    public class Channel : IDisposable
    {
        #region Network related attributes
        public PushSocket ClientSocket { get; set; }
        #endregion

        #region Channel attributes
        public SharpFly_Packet_Library.Helper.Channel ChannelData { get; set; }
        #endregion

        private Channel() { }

        public static void ProcessData(IncomingInterserverPacket packet)
        {
            packet.Position = 0;
            uint header = packet.ReadUInt();
            switch (header)
            {
                case OpCodes.REGISTER_CHANNEL_REQUEST:
                    RegisterChannelRequest(packet);
                    break;
                default:
                    Console.WriteLine(String.Format("Unknown packet header {0}", header));
                    break;
            }
        }

        public void Dispose()
        {
            ClusterServer.ChannelManager.RemoveChannel(this);
        }

        #region Incoming Packets
        public static void RegisterChannelRequest(IncomingInterserverPacket packet)
        {
            RegisterChannelRequest request = new RegisterChannelRequest(packet);

            if (request.AuthorizationPassword != (string)ClusterServer.Config.GetSetting("ClusterAuthorizationPassword"))
            {
                Console.WriteLine("Couldn't register channel on this cluster");
                return;
            }

            Channel channel = new Channel();
            channel.ClientSocket = new PushSocket(String.Format(">tcp://{0}:{1}", request.Ip, request.SendPort));
            channel.ChannelData = new SharpFly_Packet_Library.Helper.Channel();
            channel.ChannelData.Id = (uint)new Random().Next(10000, int.MaxValue);
            channel.ChannelData.Name = request.Name;
            channel.ChannelData.PlayerCount = 0;
            channel.ChannelData.MaxPlayerCount = request.MaxPlayerCount;

            ClusterServer.ChannelManager.AddChannel(channel);

            SharpFly_Packet_Library.Packets.Interserver.Outgoing.RegisterNewChannelRequest newChannelRequest = new SharpFly_Packet_Library.Packets.Interserver.Outgoing.RegisterNewChannelRequest(ClusterServer.ClusterId, channel.ChannelData.Id, request.AuthorizationPassword, request.Name, request.MaxPlayerCount, ClusterServer.LoginConnector.PublisherSocket);

            Console.WriteLine("Channel identified as {0}!", channel.ChannelData.Name);
        }
        #endregion
        #region Outgoing packets
        public void SendRegisterChannelRequestSuccesful(bool accepted, uint channelId)
        {
            new SharpFly_Packet_Library.Packets.Interserver.Outgoing.RegisterChannelRequestSuccesful(accepted, channelId, this.ClientSocket);
        }
        #endregion

    }
}
