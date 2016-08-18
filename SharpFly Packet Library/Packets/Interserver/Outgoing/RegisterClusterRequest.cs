using System.Net.Sockets;

namespace SharpFly_Packet_Library.Packets.Interserver.Outgoing
{
    public class RegisterClusterRequest
    {
        public RegisterClusterRequest(string clusterAuthorizationPassword, string clusterName, uint channelCount, string[] channelName, uint[] channelPlayerCount, uint[] channelMaxPlayerCount, Socket socket)
        {
            OutgoingPacket packet = new OutgoingPacket(OpCodes.REGISTER_CLUSTER_REQUEST);
            packet.Write(clusterAuthorizationPassword);
            packet.Write(clusterName);
            packet.Write(channelCount);
            for (int i = 0; i < channelCount; i++)
                packet.Write(channelName[i]);
            for (int i = 0; i < channelCount; i++)
                packet.Write(channelPlayerCount[i]);
            for (int i = 0; i < channelCount; i++)
                packet.Write(channelMaxPlayerCount[i]);
            packet.Send(socket);
        }
    }
}
