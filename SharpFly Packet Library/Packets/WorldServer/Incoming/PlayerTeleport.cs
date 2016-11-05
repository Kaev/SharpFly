namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PlayerTeleport
    {
        public uint TeleportingPlayerID;
        public uint TargetPlayerID;

        public PlayerTeleport(IncomingPacket packet)
        {
            TeleportingPlayerID = packet.ReadUInt();
            TargetPlayerID = packet.ReadUInt();
        }
    }
}
