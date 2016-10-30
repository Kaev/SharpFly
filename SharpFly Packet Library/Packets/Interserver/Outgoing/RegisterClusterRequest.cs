using NetMQ.Sockets;

namespace SharpFly_Packet_Library.Packets.Interserver.Outgoing
{
    public class RegisterClusterRequest
    {
        public RegisterClusterRequest(uint clusterId, string authorizationPassword, string ip, string sendPort, PublisherSocket socket)
        {
            OutgoingInterserverPacket packet = new OutgoingInterserverPacket(OpCodes.REGISTER_CLUSTER_REQUEST);
            packet.Write(clusterId);
            packet.Write(authorizationPassword);
            packet.Write(ip);
            packet.Write(sendPort);
            packet.Send(socket);
        }
    }
}
