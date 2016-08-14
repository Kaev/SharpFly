using System.Net.Sockets;

namespace SharpFly_Packet_Library.Packets.LoginServer.Outgoing
{
    public class LoginFail
    {
        public LoginFail(uint errorCode, Socket socket)
        {
            OutgoingPacket packet = new OutgoingPacket(OpCodes.LOGIN_MESSAGE);
            packet.Write(errorCode);
            packet.Send(socket);
        }
    }
}
