using System.Net.Sockets;

namespace SharpFly_Packet_Library.Packets.Interserver.Outgoing
{
    public class RegisterNewChannelRequest
    {
        public RegisterNewChannelRequest(string authorizationPassword, string name, uint playerCount, uint maxPlayerCount, Socket socket)
        {
            OutgoingPacket packet = new OutgoingPacket(OpCodes.REGISTER_CLUSTER_REQUEST);
            packet.Write(authorizationPassword);
            packet.Write(name);
            packet.Write(playerCount);
            packet.Write(maxPlayerCount);
            packet.Send(socket);
        }
    }
}
