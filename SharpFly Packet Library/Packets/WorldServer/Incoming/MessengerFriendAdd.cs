namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MessengerFriendAdd
    {
        public uint PlayerID;
        public uint TargetPlayerID;
        public byte PlayerSex;
        public byte TargetPlayerSex;
        public int PlayerJob;
        public int TargetPlayerJob;

        public MessengerFriendAdd(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            TargetPlayerID = packet.ReadUInt();
            PlayerSex = packet.ReadByte();
            TargetPlayerSex = packet.ReadByte();
            PlayerJob = packet.ReadInt();
            TargetPlayerJob = packet.ReadInt();
        }
    }
}
