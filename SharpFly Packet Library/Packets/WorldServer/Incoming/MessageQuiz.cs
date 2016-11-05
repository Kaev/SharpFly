namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class MessageQuiz
    {
        public int DefinedText;
        public bool All;
        public int Channel;
        public int Time;

        public MessageQuiz(IncomingPacket packet)
        {
            DefinedText = packet.ReadInt();
            All = packet.ReadBool();
            Channel = packet.ReadInt();
            Time = packet.ReadInt();
        }
    }
}
