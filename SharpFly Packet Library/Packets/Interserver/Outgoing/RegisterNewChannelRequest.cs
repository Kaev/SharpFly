using NetMQ.Sockets;

namespace SharpFly_Packet_Library.Packets.Interserver.Outgoing
{
    public class RegisterNewChannelRequest
    {
        public RegisterNewChannelRequest(uint clusterId, uint channelId, string authorizationPassword, string name, uint maxPlayerCount, PublisherSocket socket)
        {
            OutgoingInterserverPacket packet = new OutgoingInterserverPacket(OpCodes.REGISTER_NEW_CHANNEL);
            packet.Write(authorizationPassword);
            packet.Write(clusterId);
            packet.Write(channelId);
            packet.Write(name);
            packet.Write(maxPlayerCount);
            packet.Send(socket, "SharpFlyLogin");
        }
    }
}
