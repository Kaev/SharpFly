using SharpFly_Utility_Library.Math;

namespace SharpFly_Packet_Library.Packets.WorldServer
{
    public class PlayerMove
    {
        public Vector3<float> Position;
        public Vector3<float> PositionDelta;
        public float TurnRadius;

        public PlayerMove(IncomingPacket packet)
        {
            //TBA
        }
    }
}
