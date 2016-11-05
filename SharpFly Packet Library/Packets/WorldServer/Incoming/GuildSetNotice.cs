using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildSetNotice
    {
        public uint GuildID;
        public uint PlayerID;
        public string Notice;

        public GuildSetNotice(IncomingPacket packet)
        {
            GuildID = packet.ReadUInt();
            PlayerID = packet.ReadUInt();
            Notice = packet.ReadString();
        }
    }
}
