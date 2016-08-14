using System.Net.Sockets;

namespace SharpFly_Packet_Library.Packets.LoginServer.Outgoing
{
    public class SessionKey
    {
        public SessionKey(int sessionKey, Socket socket)
        {
            OutgoingPacket packet = new OutgoingPacket(OpCodes.SESSION_KEY);
            packet.Write(sessionKey);
            packet.Send(socket);
        }
    }
}
