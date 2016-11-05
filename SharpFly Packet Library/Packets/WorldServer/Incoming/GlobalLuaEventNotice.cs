using System.Collections.Generic;

namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GlobalLuaEventNotice
    {
        public int Size;
        public List<string> Message; 

        public GlobalLuaEventNotice(IncomingPacket packet)
        {
            Size = packet.ReadInt();

            for (int i = 0; i < Size; i++)
            {
                Message.Add(packet.ReadString());
            }
        }
    }
}
