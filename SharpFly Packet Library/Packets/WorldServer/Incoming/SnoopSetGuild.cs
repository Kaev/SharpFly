using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    class SnoopSetGuild
    {
        public uint GuildID;
        public bool Snoop;

        public SnoopSetGuild(IncomingPacket packet)
        {
            GuildID = packet.ReadUInt();
            Snoop = packet.ReadBool();
        }
    }
}
