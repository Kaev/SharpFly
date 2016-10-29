using SharpFly_Packet_Library.Helper;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SharpFly_Packet_Library.Packets.LoginServer.Outgoing
{
    public class ServerList
    {
        public ServerList(List<Cluster> clusters, string accountName, Socket socket)
        {
            OutgoingPacket packet = new OutgoingPacket(OpCodes.SERVER_LIST);
            packet.Write(0); // authkey
            packet.Write((byte)1); // accountflag
            packet.Write(accountName.ToLower());
            int clusterAndChannelCount = clusters.Count;
            foreach (Cluster cluster in clusters)
                clusterAndChannelCount += cluster.Channels.Count;
            packet.Write(clusterAndChannelCount);
            foreach (Cluster cluster in clusters)
            {
                packet.Write(cluster.ParentId);
                packet.Write(cluster.Id);
                packet.Write(cluster.Name);
                packet.Write(cluster.Ip);
                packet.Write(cluster.Is18Plus);
                packet.Write(cluster.PlayerCount);
                packet.Write(cluster.Enabled);
                packet.Write(cluster.MaxPlayerCount);

                foreach (Channel channel in cluster.Channels)
                {
                    packet.Write(channel.Parent.Id);
                    packet.Write(channel.Id);
                    packet.Write(channel.Name);
                    packet.Write(channel.Ip);
                    packet.Write(channel.Is18Plus);
                    packet.Write(channel.PlayerCount);
                    packet.Write(channel.Enabled);
                    packet.Write(channel.MaxPlayerCount);
                }
            }
            packet.Send(socket);
        }
    }
}
