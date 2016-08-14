using System.Net.Sockets;

namespace SharpFly_Packet_Library.Packets.LoginServer.Outgoing
{
    public class ServerList
    {
        public ServerList(Socket socket)
        {
            OutgoingPacket packet = new OutgoingPacket(OpCodes.SERVER_LIST);

            packet.Send(socket);
        }
    }
}
