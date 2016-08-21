using System.Net.Sockets;

namespace SharpFly_Packet_Library.Packets.WorldServer.Outgoing
{
    public class ServerIp
    {
        public ServerIp(string ipAddress, Socket socket)
        {
            OutgoingPacket packet = new OutgoingPacket(OpCodes.SERVER_IP);
            packet.Write(ipAddress);
            packet.Send(socket);
        }
    }
}
