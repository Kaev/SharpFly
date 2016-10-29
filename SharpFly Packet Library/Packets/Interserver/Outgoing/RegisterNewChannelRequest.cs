using NetMQ.Sockets;

namespace SharpFly_Packet_Library.Packets.Interserver.Outgoing
{
    public class RegisterNewChannelRequest
    {
        public RegisterNewChannelRequest(string authorizationPassword, string name, uint clusterId, uint playerCount, uint maxPlayerCount, PushSocket socket)
        {
            OutgoingInterserverPacket packet = new OutgoingInterserverPacket(OpCodes.REGISTER_NEW_CHANNEL);
            packet.Write(authorizationPassword);
            packet.Write(clusterId);
            packet.Write(name);
            packet.Write(playerCount);
            packet.Write(maxPlayerCount);
            packet.Send(socket);
        }
    }
}
