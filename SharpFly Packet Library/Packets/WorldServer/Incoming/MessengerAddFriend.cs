namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MessengerAddFriend
    {
        public uint PlayerID;
        public uint TargetPlayerID;
        public byte PlayerSex;
        public byte TargetPlayerSex;
        public int PlayerJob;
        public int TargetPlayerJob;

        public MessengerAddFriend(IncomingPacket packet)
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
