using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PartySendName
    {
        public uint PartyNameSize;
        public uint PlayerID;
        public string PartyName;

        /// <summary>
        /// Difference PartySendName and PartyChangeName: PartySendName is being used when sending Information to the WorldServer 
        /// where PartyChangeName is being used when Changing the party name (converting to advanced Party).
        /// </summary>
        public PartySendName(IncomingPacket packet)
        {
            PartyNameSize = packet.ReadUInt();
            PlayerID = packet.ReadUInt();
            PartyName = packet.ReadString();
        }
    }
}
