using SharpFly_Utility_Library.Math;

namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PlayerSummon
    {
        public uint SummoningPlayerID;
        public uint IDOfMulti;
        public uint WorldID;
        public Vector3<float> Position;
        public uint TargetPlayerID;
        public int Layer;

        public PlayerSummon(IncomingPacket packet)
        {
            SummoningPlayerID = packet.ReadUInt();
            IDOfMulti = packet.ReadUInt();
            WorldID = packet.ReadUInt();
            Position = new Vector3<float>(packet.ReadFloat(), packet.ReadFloat(), packet.ReadFloat());
            TargetPlayerID = packet.ReadUInt();
            Layer = packet.ReadInt();
        }
    }
}
