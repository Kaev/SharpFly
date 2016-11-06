using NetMQ.Sockets;

namespace SharpFly_Packet_Library.Packets.Interserver.Outgoing
{
    public class RegisterChannelRequest
    {
        public RegisterChannelRequest(string authorizationPassword, string ip, string sendPort, string name, uint maxPlayerCount, PublisherSocket socket)
        {
            OutgoingInterserverPacket packet = new OutgoingInterserverPacket(OpCodes.REGISTER_CHANNEL_REQUEST);
            packet.Write(authorizationPassword);
            packet.Write(ip);
            packet.Write(sendPort);
            packet.Write(name);
            packet.Write(maxPlayerCount);
            packet.Send(socket, "SharpFlyCluster");
        }
    }
}
