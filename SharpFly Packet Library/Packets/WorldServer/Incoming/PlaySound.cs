namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PlaySound
    {
        public uint IDOfMulti; //Guess that's the Multi channel stuff
        public uint WorldID;
        public uint SoundID;

        public PlaySound(IncomingPacket packet)
        {
            IDOfMulti = packet.ReadUInt();
            WorldID = packet.ReadUInt();
            SoundID = packet.ReadUInt();
        }
    }
}
