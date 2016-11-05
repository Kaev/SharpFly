namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PlayerBlock
    {
        public byte BlockKind;          //ChatBlocked, TradeBlocked, there are several types, probably will change to Enum later
        public uint TargetPlayerID;
        public uint PlayerID;

        public PlayerBlock(IncomingPacket packet)
        {
            BlockKind = packet.ReadByte();
            TargetPlayerID = packet.ReadUInt();
            PlayerID = packet.ReadUInt();
        }
    }
}
