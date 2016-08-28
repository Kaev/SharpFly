using System;
using System.Net.Sockets;

namespace SharpFly_Packet_Library.Packets.ClusterServer.Outgoing
{
    public class Pong
    {
        public Pong(int time, Socket socket)
        {
            OutgoingPacket packet = new OutgoingPacket(OpCodes.PONG);
            packet.Write(time);
            packet.Send(socket);
        }
    }
}
