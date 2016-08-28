using System.Net.Sockets;

namespace SharpFly_Packet_Library.Packets.ClusterServer.Outgoing
{
    public class CharacterList
    {
        public CharacterList(uint TimeGetTime, Socket socket)
        {
            OutgoingPacket packet = new OutgoingPacket(OpCodes.CHARACTER_LIST);
            packet.Write(TimeGetTime);

            // if 0 characters
            packet.Write(0);
            packet.Write(0);
            packet.Send(socket);

            // if >0 characters
        }
    }
}
