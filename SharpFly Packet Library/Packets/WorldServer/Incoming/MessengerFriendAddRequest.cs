using SharpFly_Utility_Library.Enums;

namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MessengerFriendAddRequest
    {
        public uint InvitingPlayerID;
        public uint TargetPlayerID;
        public int InvitingPlayerJob;
        public Gender InvitingPlayerSex;
        public string InvitingPlayerName;
        public string TargetPlayerName;

        public MessengerFriendAddRequest(IncomingPacket packet)
        {
            InvitingPlayerID = packet.ReadUInt();
            TargetPlayerID = packet.ReadUInt();
            InvitingPlayerJob = packet.ReadInt();
            InvitingPlayerSex = (Gender)packet.ReadByte();
            InvitingPlayerName = packet.ReadString();
            TargetPlayerName = packet.ReadString();
        }
    }
}
