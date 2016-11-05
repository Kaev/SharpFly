using SharpFly_Utility_Library.Database.ClusterDatabase.Tables;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SharpFly_Packet_Library.Packets.ClusterServer.Outgoing
{
    public class CharacterList
    {
        public CharacterList(uint authKey, Dictionary<CharacterSlot, Character> characters, Socket socket)
        {
            // not finished

            OutgoingPacket packet = new OutgoingPacket(OpCodes.CHARACTER_LIST);
            packet.Write(authKey); // authkey
            packet.Write(characters.Keys.Count); // character count

            // if > 0 characters
            if (characters.Keys.Count > 0)
            {
                foreach (KeyValuePair<CharacterSlot, Character> character in characters)
                {
                    packet.Write(character.Key.SlotId);
                    packet.Write(2); // Character block
                    packet.Write(character.Value.Map);
                    packet.Write(0x0B); // Index? 0x0B?
                    packet.Write(character.Value.Name);
                    packet.Write(character.Value.Position.X);
                    packet.Write(character.Value.Position.Y);
                    packet.Write(character.Value.Position.Z);
                    packet.Write(character.Value.CharacterId);
                    packet.Write(0); // ID Party
                    packet.Write(0); // ID Guild
                    packet.Write(0); // ID War
                    packet.Write(character.Value.Skinset);
                    packet.Write(character.Value.HairStyle);
                    packet.Write(character.Value.HairColor);
                    packet.Write(character.Value.HeadMesh);
                    packet.Write(character.Value.Gender);
                    packet.Write(character.Value.ClassId);
                    packet.Write(character.Value.Level);
                    packet.Write(0); // Job level
                    packet.Write(character.Value.Strength);
                    packet.Write(character.Value.Stamina);
                    packet.Write(character.Value.Dexterity);
                    packet.Write(character.Value.Intelligence);
                    packet.Write(0); // Mode
                    packet.Write(0); // equipcount

                    // for each item
                    if (false)
                    {
                        packet.Write(0); // itemid
                    }
                }
            }

            packet.Write(0); // countmessenger
            // for each in countmessenger
            if (false)
            {
                packet.Write(0); // slot? (byte)
            }

            packet.Send(socket);
        }
    }
}
