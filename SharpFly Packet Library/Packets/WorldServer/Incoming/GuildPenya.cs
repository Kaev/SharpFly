namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildPenya
    {
        public uint GuildID;
        public int Penya;

        public GuildPenya(IncomingPacket packet)
        {
            GuildID = packet.ReadUInt();
            Penya = packet.ReadInt();
        }
    }
}
