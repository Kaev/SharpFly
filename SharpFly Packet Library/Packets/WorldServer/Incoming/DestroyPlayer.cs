namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class DestroyPlayer
    {
        public uint PlayerID;

        public DestroyPlayer(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
        }
    }
}
