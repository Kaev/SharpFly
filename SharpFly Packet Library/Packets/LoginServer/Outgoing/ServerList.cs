using System.Net.Sockets;

namespace SharpFly_Packet_Library.Packets.LoginServer.Outgoing
{
    public class ServerList
    {
        public ServerList(Socket socket)
        {
            OutgoingPacket packet = new OutgoingPacket(OpCodes.SERVER_LIST);
            packet.Write((byte)1); // dafuq is this
            packet.Write(""); // accountname in lower, maybe
            packet.Write(0); // server count
            // for each cluster
            packet.Write(-1); // dafuq is this
            packet.Write(1); // cluster id;
            packet.Write(""); // cluster name
            packet.Write(""); // cluster ip
            packet.Write(1); // dafuq is this * 4?
            packet.Write(1);
            packet.Write(1);
            packet.Write(1);

            // for each channel in cluster
            packet.Write(1); // cluster id
            packet.Write(1); // channel id
            packet.Write(""); // cluster name
            packet.Write(0); // dafuq is this
            packet.Write(0); // dafuq is this
            packet.Write(1); // current amount of players
            packet.Write(1); // dafuq is this
            packet.Write(1); // max player count on channel
            // end for each channel in cluster

            // end for each cluster
            packet.Send(socket);
        }
    }
}
