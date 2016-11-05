using SharpFly_Utility_Library.Math;

namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class BountyReward
    {
        public uint KillerPlayerID;
        public uint VictimPlayerID; //Not sure bout that.
        public uint WorldID;
        public Vector3<float> Position;
        public string Message;

        public BountyReward(IncomingPacket packet)
        {
            KillerPlayerID = packet.ReadUInt();
            VictimPlayerID = packet.ReadUInt();
            WorldID = packet.ReadUInt();
            Position = new Vector3<float>(packet.ReadFloat(), packet.ReadFloat(), packet.ReadFloat());
            Message = packet.ReadString();
        }
    }
}
