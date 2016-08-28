using System;
using System.Collections.Generic;
using System.Net.Sockets;
using SharpFly_Packet_Library.Packets;
using SharpFly_Login.Server;
using SharpFly_Packet_Library.Packets.Interserver.Incoming;
using SharpFly_Packet_Library.Helper;

namespace SharpFly_Login.Clusters
{
    public class Cluster
    {
        private const uint m_InterserverPacketOpCodePosition = 5;


        #region "Network related attributes"
        private byte[] m_RemainingBytes = null;
        public byte[] Buffer { get; set; }
        public const int BufferSize = 1500;
        public List<byte> ReceivedBytes { get; set; }
        public Socket Socket { get; set; }
        #endregion

        #region "Cluster attributes"
        public SharpFly_Packet_Library.Helper.Cluster ClusterData;
        #endregion

        private Cluster() { }

        public Cluster(Socket socket)
        {
            this.Buffer = new byte[BufferSize];
            ReceivedBytes = new List<byte>();
            this.Socket = socket;
            Console.WriteLine("New cluster server " + this.Socket.RemoteEndPoint + " connected!");
        }

        public void ProcessData()
        {
            try
            {
                if (this.Socket != null)
                {
                    if (this.Socket.Available <= 0)
                        return;

                    int byteCount = this.Socket.Receive(this.Buffer, this.Buffer.Length, SocketFlags.None);
                    if (byteCount <= 0)
                        return;
                    ReceivedBytes.AddRange(this.Buffer);
                }

                IncomingPacket packet = new IncomingPacket(ReceivedBytes.ToArray());

                if (packet == null)
                    return;

                if (m_RemainingBytes != null)
                {
                    byte[] buffer = new byte[m_RemainingBytes.Length + packet.Buffer.Length];
                    Array.Copy(m_RemainingBytes, 0, buffer, 0, m_RemainingBytes.Length);
                    Array.Copy(packet.Buffer, 0, buffer, 0, packet.Buffer.Length);
                    packet.Buffer = buffer;
                    m_RemainingBytes = null;
                }
                else
                {
                    packet.Position = m_InterserverPacketOpCodePosition;
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
                ReceivedBytes.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Dispose()
        {
            LoginServer.ClusterManager.RemoveCluster(this);
            this.Socket.Shutdown(SocketShutdown.Both);
            this.Socket.Close();
        }

        #region Incoming packets
        public void RegisterClusterRequest(IncomingPacket packet)
        {
            RegisterClusterRequest request = new RegisterClusterRequest(packet);
            if (request.AuthorizationPassword != (string)LoginServer.Config.GetSetting("ClusterAuthorizationPassword"))
            {
                this.Dispose();
                return;
            }

            this.ClusterData = new SharpFly_Packet_Library.Helper.Cluster();
            this.ClusterData.Id = request.Id;
            this.ClusterData.Name = request.Name;
            this.ClusterData.Ip = request.Ip;

            Console.WriteLine("Cluster identified as {0}!", this.ClusterData.Name);
        }

        public void RegisterNewChannel(IncomingPacket packet)
        {
            RegisterNewChannelRequest request = new RegisterNewChannelRequest(packet);

            Channel channel = new Channel();
            channel.Parent = this.ClusterData;
            channel.Id = ClusterManager.Id++;
            channel.Name = request.Name;
            channel.PlayerCount = request.PlayerCount;
            channel.MaxPlayerCount = request.MaxPlayerCount;

            this.ClusterData.Channels.Add(channel);

            Console.WriteLine("Registered channel {0} on cluster {1}", channel.Name, this.ClusterData.Name);
        }
        #endregion
        #region Outgoing packets

        #endregion
    }
}