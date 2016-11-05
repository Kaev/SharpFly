namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildMemberSetClass
    {
        public byte Flag;       // UP : 1, DOWN : 0
        public uint MasterID;
        public uint PlayerID;

        public GuildMemberSetClass(IncomingPacket packet)
        {
            Flag = packet.ReadByte();
            MasterID = packet.ReadUInt();
            PlayerID = packet.ReadUInt();
        }
    }
}
