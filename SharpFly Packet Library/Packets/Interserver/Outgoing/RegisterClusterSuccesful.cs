using NetMQ.Sockets;

namespace SharpFly_Packet_Library.Packets.Interserver.Outgoing
{
    public class RegisterClusterSuccesful
    {
        public RegisterClusterSuccesful(uint clusterId, bool succesful, PushSocket socket)
        {
            OutgoingInterserverPacket packet = new OutgoingInterserverPacket(OpCodes.REGISTER_CLUSTER_REQUEST_SUCCESFUL);
            packet.Write(clusterId);
            packet.Write(succesful);
            packet.Send(socket);
        }
    }
}
