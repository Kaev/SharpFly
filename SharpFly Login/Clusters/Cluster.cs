using System;
using System.Collections.Generic;
using System.Net.Sockets;
using SharpFly_Packet_Library.Packets;
using SharpFly_Login.Server;
using SharpFly_Packet_Library.Packets.Interserver.Incoming;
using SharpFly_Packet_Library.Helper;

namespace SharpFly_Login.Clusters
{
    public class Cluster : IDisposable
    {
        #region "Cluster attributes"
        public SharpFly_Packet_Library.Helper.Cluster ClusterData;
        #endregion

        private Cluster() { }

        public static void ProcessData(IncomingInterserverPacket packet)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            if (request.AuthorizationPassword != (string)LoginServer.Config.GetSetting("ClusterAuthorizationPassword"))
                return;

            Cluster cluster = new Cluster();
            cluster.ClusterData = new SharpFly_Packet_Library.Helper.Cluster();
            cluster.ClusterData.Id = request.Id;
            cluster.ClusterData.Name = request.Name;
            cluster.ClusterData.Ip = request.Ip;
            LoginServer.ClusterManager.AddCluster(cluster);

            Console.WriteLine("Cluster identified as {0}!", cluster.ClusterData.Name);
        }

        public static void RegisterNewChannel(IncomingInterserverPacket packet)
        {
            RegisterNewChannelRequest request = new RegisterNewChannelRequest(packet);

            uint clusterId = request.ClusterId;
            Cluster cluster = LoginServer.ClusterManager.GetClusterById(clusterId);

            if (cluster != null)
            {
                Channel channel = new Channel();
                channel.Parent = cluster.ClusterData;
                channel.Id = ClusterManager.Id++;
                channel.Name = request.Name;
                channel.PlayerCount = request.PlayerCount;
                channel.MaxPlayerCount = request.MaxPlayerCount;
                cluster.ClusterData.Channels.Add(channel);

                Console.WriteLine("Registered channel {0} on cluster {1}", channel.Name, cluster.ClusterData.Name);
            }
            else
                Console.WriteLine("There is no cluster with the id {0}", clusterId.ToString());
        }
        #endregion
        #region Outgoing packets

        #endregion
    }
}