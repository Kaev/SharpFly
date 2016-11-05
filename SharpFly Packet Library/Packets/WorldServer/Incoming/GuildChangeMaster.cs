namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildChangeMaster
    {
        public uint CurrMasterID;
        public uint NewMasterID;

        public GuildChangeMaster(IncomingPacket packet)
        {
            CurrMasterID = packet.ReadUInt();
            NewMasterID = packet.ReadUInt();
        }
    }
}
