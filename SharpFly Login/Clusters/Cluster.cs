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
            Console.WriteLine("New world server " + this.Socket.RemoteEndPoint + " connected!");
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
            if (request.ClusterAuthorizationPassword != (string)LoginServer.Config.GetSetting("ClusterAuthorizationPassword"))
            {
                this.Dispose();
                return;
            }

            this.ClusterData = new SharpFly_Packet_Library.Helper.Cluster();
            this.ClusterData.Id = ClusterManager.Id++;
            this.ClusterData.Ip = "127.0.0.1";
            this.ClusterData.Name = request.ClusterName;
            this.ClusterData.ChannelCount = request.ChannelCount;
            this.ClusterData.Channel = new Channel[this.ClusterData.ChannelCount];
            for(int i = 0; i < this.ClusterData.ChannelCount; i++)
            {
                this.ClusterData.Channel[i] = new Channel();
                this.ClusterData.Channel[i].Id = ClusterManager.Id++;
                this.ClusterData.Channel[i].Name = request.ChannelName[i];
                this.ClusterData.Channel[i].Parent = this.ClusterData;
                this.ClusterData.Channel[i].PlayerCount = request.ChannelPlayerCount[i];
                this.ClusterData.Channel[i].MaxPlayerCount = request.ChannelMaxPlaxerCount[i];
            }
            

            Console.WriteLine("Cluster identified as {0}!", this.ClusterData.Name);
        }

        public void UpdateClusterChannelPlayerCount(IncomingPacket packet)
        {
            UpdateClusterChannelPlayerCount request = new UpdateClusterChannelPlayerCount(packet);
            if (request.ChannelId <= this.ClusterData.ChannelCount)
                this.ClusterData.Channel[request.ChannelId].PlayerCount = request.NewPlayerCount;
        }
        #endregion
        #region Outgoing packets

        #endregion
    }
}