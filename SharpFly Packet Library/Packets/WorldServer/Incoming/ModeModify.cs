using SharpFly_Utility_Library.Enums;

namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class ModeModify
    {
        public Mode mode;
        public byte AddMode;     // If 1: Add Mode If 0 Replace

        public ModeModify(IncomingPacket packet)
        {
            mode = (Mode)packet.ReadUInt();
            AddMode = packet.ReadByte();
        }
    }
}
