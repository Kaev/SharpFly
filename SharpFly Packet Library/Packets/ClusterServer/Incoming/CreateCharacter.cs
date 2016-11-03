using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFly_Packet_Library.Packets.ClusterServer.Incoming
{
    public class CreateCharacter
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public byte Slot { get; private set; }
        public string CharacterName { get; private set; }
        public byte Face { get; private set; }
        public byte Costume { get; private set; } // Unused
        public byte Skinset { get; private set; } // Unused
        public byte HairStyle { get; private set; }
        public uint HairColor { get; private set; }
        public byte Gender { get; private set; }
        public byte Class { get; private set; } // Should always be 0
        public byte HeadMesh { get; private set; }
        public int BankPw { get; private set; }
        public int AuthKey { get; private set; }

        public CreateCharacter(IncomingPacket packet)
        {
            Username = packet.ReadString();
            Password = packet.ReadString();
            Slot = packet.ReadByte();
            CharacterName = packet.ReadString();
            Face = packet.ReadByte();
            Costume = packet.ReadByte();
            Skinset = packet.ReadByte();
            HairStyle = packet.ReadByte();
            HairColor = packet.ReadUInt();
            Gender = packet.ReadByte();
            Class = packet.ReadByte();
            HeadMesh = packet.ReadByte();
            BankPw = packet.ReadInt();
            AuthKey = packet.ReadInt();
        }

    }
}
