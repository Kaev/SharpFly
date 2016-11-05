using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFly_Packet_Library.Packets.ClusterServer.Incoming
{
    public class DeleteCharacter
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public int CharacterId { get; set; }
        public uint AuthKey { get; set; }
        public uint MessengerSlotLength { get; set; }
        public List<uint> MessengerSlots { get; set; }

        public DeleteCharacter(IncomingPacket packet)
        {
            MessengerSlots = new List<uint>();
            Username = packet.ReadString();
            Password = packet.ReadString();
            PasswordConfirmation = packet.ReadString();
            CharacterId = packet.ReadInt();
            AuthKey = packet.ReadUInt();
            MessengerSlotLength = packet.ReadUInt();
            for (int i = 0; i < MessengerSlotLength; i++)
                MessengerSlots.Add(packet.ReadUInt());
        }
    }
}
