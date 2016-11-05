namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildWarJoinLeaveArena
    {
        public uint PlayerID;

        public GuildWarJoinLeaveArena(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
        }
    }
}
