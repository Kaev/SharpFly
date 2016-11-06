using NetMQ.Sockets;

namespace SharpFly_Packet_Library.Packets.Interserver.Outgoing
{
    public class RegisterChannelRequestSuccesful
    {
        public RegisterChannelRequestSuccesful(bool accepted, uint channelId, PushSocket socket)
        {
            OutgoingInterserverPacket packet = new OutgoingInterserverPacket(OpCodes.REGISTER_CHANNEL_REQUEST_SUCCESFUL);
            packet.Write(accepted);
            packet.Write(channelId);
            packet.Send(socket);
        }
    }
}
