namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildCombatState
    {
        public int State;

        public GuildCombatState(IncomingPacket packet)
        {
            State = packet.ReadInt();
        }
    }
}
