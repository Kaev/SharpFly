namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GameRate
    {
        public float Rate;
        public byte Flag;           //Different scale factors like Monster Hp, Exp and stuff. See void CDPCoreClient::OnGameRate( CAr & ar, DPID, DPID, OBJID ) for more information

        public GameRate(IncomingPacket packet)
        {
            Rate = packet.ReadFloat();
            Flag = packet.ReadByte();
        }
    }
}
