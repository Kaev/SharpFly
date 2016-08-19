using SharpFly_Packet_Library.Helper;
using System.Net.Sockets;

namespace SharpFly_Packet_Library.Packets.LoginServer.Outgoing
{
    public class ServerList
    {
        public ServerList(Cluster[] clusters, string accountName, Socket socket)
        {
            OutgoingPacket packet = new OutgoingPacket(OpCodes.SERVER_LIST);
            packet.Write(0); // authkey?
            packet.Write((byte)1); // accountflag
            packet.Write(accountName.ToLower()); // accountname in lower
            uint clusterAndChannelCount = (uint)clusters.Length;
            for (int i = 0; i < clusters.Length; i++)
                clusterAndChannelCount += clusters[i].ChannelCount;
            packet.Write(clusterAndChannelCount); // number of cluster + channel
            for (int i = 0; i < clusters.Length; i++)
            {
                packet.Write(-1); // parent id - -1 because cluster
                packet.Write(clusters[i].Id); // cluster id;
                packet.Write(clusters[i].Name); // cluster name
                packet.Write(clusters[i].Ip); // cluster ip
                packet.Write(0); // named 18 on official - probably unused
                packet.Write(0); // player count on channel - probably unused on cluster
                packet.Write(1); // enable - cluster too? probably unused
                packet.Write(0); // max player count on channel - probably unused on cluster

                for(int j = 0; j < clusters[i].ChannelCount; j++)
                {
                    packet.Write(clusters[i].Channel[j].Parent.Id); // cluster id
                    packet.Write(clusters[i].Channel[j].Id); // channel id
                    packet.Write(clusters[i].Channel[j].Name); // channel name
                    packet.Write(0); // ip - only used in cluster
                    packet.Write(0); // named 18 on official - probably unused
                    packet.Write(clusters[i].Channel[j].PlayerCount); // current amount of players
                    packet.Write(1); // enable in official - enable cluster?
                    packet.Write(clusters[i].Channel[j].MaxPlayerCount); // max player count on channel
                }
            }
            packet.Send(socket);
        }
    }
}
