using System;
using SharpFly_Packet_Library.Packets;
using SharpFly_Login.Server;
using SharpFly_Packet_Library.Helper;
using NetMQ.Sockets;
using SharpFly_Packet_Library.Packets.Interserver.Incoming;
using System.Threading;

namespace SharpFly_Login.Clusters
{
    public class Cluster : IDisposable
    {
        #region "Network related attributes"
        public PushSocket ClientSocket { get; set; }
        #endregion

        #region "Cluster attributes"
        public SharpFly_Packet_Library.Helper.Cluster ClusterData { get; set; }
        #endregion

        private Cluster() { }

        public static void ProcessData(IncomingInterserverPacket packet)
        {
            packet.Position = 0;
            uint header = packet.ReadUInt();
            switch (header)
            {
                case OpCodes.REGISTER_CLUSTER_REQUEST:
                    RegisterClusterRequest(packet);
                    break;
                case OpCodes.REGISTER_NEW_CHANNEL:
                    RegisterNewChannel(packet);
                    break;
                default:
                    Console.WriteLine(String.Format("Unknown packet header {0}", header));
                    break;
            }
        }

        public void Dispose()
        {
            LoginServer.ClusterManager.RemoveCluster(this);
        }

        #region Incoming packets
        public static void RegisterClusterRequest(IncomingInterserverPacket packet)
        {
            RegisterClusterRequest request = new RegisterClusterRequest(packet);

            SharpFly_Utility_Library.Database.LoginDatabase.Tables.Cluster clusterTable = SharpFly_Utility_Library.Database.LoginDatabase.Queries.Cluster.Instance.GetCluster(request.ClusterId);

            Cluster cluster = new Cluster();
            cluster.ClientSocket = new PushSocket(String.Format(">tcp://{0}:{1}", LoginServer.Config.GetSetting("LoginAddress"), request.SendPort));
            cluster.ClusterData = new SharpFly_Packet_Library.Helper.Cluster();
            cluster.ClusterData.Id = request.ClusterId;
            cluster.ClusterData.Name = clusterTable.Name;
            cluster.ClusterData.Ip = request.Ip;

            if (clusterTable == null)
            {
                cluster.SendRegisterClusterSuccesful(0, false);
                Console.WriteLine("Couldn't register cluster {0}: The cluster id {1} wasn't found in the database", cluster.ClusterData.Name, cluster.ClusterData.Id);
                return;
            }
            if (request.AuthorizationPassword != clusterTable.Password)
            {
                cluster.SendRegisterClusterSuccesful(0, false);
                Console.WriteLine("Couldn't register cluster {0}: Cluster authorization password was wrong", cluster.ClusterData.Name);
                return;
            }

            LoginServer.ClusterManager.AddCluster(cluster);
            cluster.SendRegisterClusterSuccesful(cluster.ClusterData.Id, true);

            Console.WriteLine("Cluster identified as {0}!", cluster.ClusterData.Name);
        }

        public static void RegisterNewChannel(IncomingInterserverPacket packet)
        {
            RegisterNewChannelRequest request = new RegisterNewChannelRequest(packet);

            SharpFly_Utility_Library.Database.LoginDatabase.Tables.Cluster clusterTable = SharpFly_Utility_Library.Database.LoginDatabase.Queries.Cluster.Instance.GetCluster(request.ClusterId);

            uint clusterId = request.ClusterId;
            Cluster cluster = LoginServer.ClusterManager.GetClusterById(clusterId);

            if (clusterTable == null || request.AuthorizationPassword != clusterTable.Password)
            {
                cluster.SendRegisterNewChannelSuccesful(0, false);
                Console.WriteLine("Couldn't register new channel to cluster {1}", cluster.ClusterData.Name);
                return;
            }

            if (cluster != null)
            {
                Channel channel = new Channel();
                channel.Parent = cluster.ClusterData;
                channel.Id = ClusterManager.Id++;
                channel.Name = request.Name;
                channel.PlayerCount = request.PlayerCount;
                channel.MaxPlayerCount = request.MaxPlayerCount;
                cluster.ClusterData.Channels.Add(channel);

                cluster.SendRegisterNewChannelSuccesful(channel.Id, true);
                Console.WriteLine("Registered channel {0} on cluster {1}", channel.Name, cluster.ClusterData.Name);
                return;
            }

            Console.WriteLine("Couldn't register new channel to cluster {1}", cluster.ClusterData.Name);
        }
        #endregion
        #region Outgoing packets
        public void SendRegisterClusterSuccesful(uint clusterId, bool succesful)
        {
            new SharpFly_Packet_Library.Packets.Interserver.Outgoing.RegisterClusterSuccesful(clusterId, succesful, this.ClientSocket);
        }

        public void SendRegisterNewChannelSuccesful(uint channelId, bool succesful)
        {
            new SharpFly_Packet_Library.Packets.Interserver.Outgoing.RegisterNewChannelSuccesful(channelId, succesful, this.ClientSocket);
        }
        #endregion
    }
}