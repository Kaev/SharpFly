namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildCombatDisbandParty
    {
        public uint PartyID;
        public uint PlayerID;

        public GuildCombatDisbandParty(IncomingPacket packet)
        {
            PartyID = packet.ReadUInt();
            PlayerID = packet.ReadUInt();
        }
    }
}
