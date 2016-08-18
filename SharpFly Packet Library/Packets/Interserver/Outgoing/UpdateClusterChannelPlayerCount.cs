using System.Net.Sockets;

namespace SharpFly_Packet_Library.Packets.Interserver.Outgoing
{
    public class UpdateClusterChannelPlayerCount
    {
        public UpdateClusterChannelPlayerCount(uint channelId, uint playerCount, Socket socket)
        {
            OutgoingPacket packet = new OutgoingPacket(OpCodes.UPDATE_CLUSTER_CHANNEL_PLAYER_COUNT);
            packet.Write(channelId);
            packet.Write(playerCount);
            packet.Send(socket);
        }
    }
}
