namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PlayerKill
    {
        public uint KillerPlayerID;
        public uint VictimPlayerID;

        public PlayerKill(IncomingPacket packet)
        {
            KillerPlayerID = packet.ReadUInt();
            VictimPlayerID = packet.ReadUInt();
        }
    }
}
