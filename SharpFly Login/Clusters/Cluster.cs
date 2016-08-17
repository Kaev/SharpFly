using System;
using System.Collections.Generic;
using System.Net.Sockets;
using SharpFly_Packet_Library.Packets;
using SharpFly_Login.Server;

namespace SharpFly_Login.Clusters
{
    class Cluster
    {
        private byte[] m_RemainingBytes = null;

        public byte[] Buffer { get; set; }
        public const int BufferSize = 1500;
        public List<byte> ReceivedBytes { get; set; }
        public Socket Socket { get; set; }

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

                byte[] data = ReceivedBytes.ToArray();
                IncomingPacket[] packets = IncomingPacket.SplitPackets(data);
                foreach (IncomingPacket packet in packets)
                {
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
                        packet.Position = 13; // Go to headers
                        uint header = (uint)packet.ReadInt();
                        switch (header)
                        {
                            default:
                                Console.WriteLine(String.Format("Unknown packet header {0}", header));
                                break;
                        }
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
        public void ConnectRequest(IncomingPacket packet)
        {

        }
        #endregion
        #region Outgoing packets

        #endregion
    }
}