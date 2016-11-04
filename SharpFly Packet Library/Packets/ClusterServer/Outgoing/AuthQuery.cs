using System.Net.Sockets;

namespace SharpFly_Packet_Library.Packets.ClusterServer.Outgoing
{
    public class AuthQuery
    {
        public AuthQuery(int value1, int value2, int value3, int value4, Socket socket)
        {
            OutgoingPacket packet = new OutgoingPacket(OpCodes.PONG);
            packet.Write(value1);
            packet.Write(value2);
            packet.Write(value3);
            packet.Write(value4);
            packet.Send(socket);
        }
    }
}
