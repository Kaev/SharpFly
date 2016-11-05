using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PlayerChangeName
    {
        public uint PlayerID;
        public string PlayerName;
        public uint ObjID;              //Maybe ID of Item(Scroll of Namechange)?
        public bool IsNameChange;

        public PlayerChangeName(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            PlayerName = packet.ReadString();
            ObjID = packet.ReadUInt();
            IsNameChange = packet.ReadBool();
        }
    }
}
