using System.Net.Sockets;

namespace SharpFly_Packet_Library.Packets.ClusterServer.Outgoing
{
    public class CharacterList
    {
        public CharacterList(uint TimeGetTime, Socket socket)
        {
            // not finished

            OutgoingPacket packet = new OutgoingPacket(OpCodes.CHARACTER_LIST);
            packet.Write(TimeGetTime); // authkey
            packet.Write(0); // character count

            // if > 0 characters
            if (false)
            {
                packet.Write(0); // slot
                packet.Write(0); // character block? 1
                packet.Write(0); // WorldId? 1
                packet.Write(0); // Index? 0x0b?
                packet.Write(0); // Charname
                packet.Write(0); // x
                packet.Write(0); // y
                packet.Write(0); // z
                packet.Write(0); // idplayer
                packet.Write(0); // idparty 0
                packet.Write(0); // idguild 0
                packet.Write(0); // idwar 0
                packet.Write(0); // skinset 0
                packet.Write(0); // hairmesh
                packet.Write(0); // haircolor
                packet.Write(0); // headmesh
                packet.Write(0); // gender
                packet.Write(0); // class
                packet.Write(0); // level
                packet.Write(0); // joblevel? 0
                packet.Write(0); // str
                packet.Write(0); // sta
                packet.Write(0); // dex
                packet.Write(0); // int
                packet.Write(0); // mode? 0
                packet.Write(0); // equipcount

                // for each item
                if(false)
                {
                    packet.Write(0); // itemid
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
