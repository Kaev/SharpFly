using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildMemberJoinDate
    {
        public uint PlayerID;
        public string JoinTime;

        public GuildMemberJoinDate(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            JoinTime = packet.ReadString();
        }
    }
}
