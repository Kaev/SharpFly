using System;
using System.Net.Sockets;

namespace SharpFly_Packet_Library.Packets.ClusterServer.Outgoing
{
    public class QueryTickCount
    {
        public QueryTickCount(int time, Socket socket)
        {
            OutgoingPacket packet = new OutgoingPacket(OpCodes.QUERY_TICK_COUNT);
            packet.Write(time);
            packet.Write(DateTime.Now.Ticks);
            packet.Send(socket);
        }
    }
}
