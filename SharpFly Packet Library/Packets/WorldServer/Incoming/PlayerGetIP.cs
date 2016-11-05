namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PlayerGetIP
    {
        public uint PlayerID;
        public uint TargetPlayerID;

        public PlayerGetIP(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            TargetPlayerID = packet.ReadUInt();
        }
    }
}
