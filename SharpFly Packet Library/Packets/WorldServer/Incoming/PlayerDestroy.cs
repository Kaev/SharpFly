namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PlayerDestroy
    {
        public uint PlayerID;

        public PlayerDestroy(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
        }
    }
}
