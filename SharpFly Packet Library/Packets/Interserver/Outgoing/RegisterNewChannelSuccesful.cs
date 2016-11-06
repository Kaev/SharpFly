using NetMQ.Sockets;

namespace SharpFly_Packet_Library.Packets.Interserver.Outgoing
{
    public class RegisterNewChannelSuccesful
    {
        public RegisterNewChannelSuccesful(uint channelId, uint tempChannelId, bool succesful, PushSocket socket)
        {
            OutgoingInterserverPacket packet = new OutgoingInterserverPacket(OpCodes.REGISTER_NEW_CHANNEL_SUCCESFUL);
            packet.Write(channelId);
            packet.Write(tempChannelId);
            packet.Write(succesful);
            packet.Send(socket);
        }
    }
}
