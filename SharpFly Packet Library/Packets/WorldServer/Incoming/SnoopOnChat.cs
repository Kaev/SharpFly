using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class SnoopOnChat
    {
        public uint PlayerID1;
        public uint PlayerID2;
        public string Message;

        public SnoopOnChat(IncomingPacket packet)
        {
            PlayerID1 = packet.ReadUInt();
            PlayerID2 = packet.ReadUInt();
            Message = packet.ReadString();
        }
    }
}
