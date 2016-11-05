namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PartyRemovePoint
    {
        public uint PartyID;
        public int RemovePointAmount;

        public PartyRemovePoint(IncomingPacket packet)
        {
            PartyID = packet.ReadUInt();
            RemovePointAmount = packet.ReadInt();
        }
    }
}
