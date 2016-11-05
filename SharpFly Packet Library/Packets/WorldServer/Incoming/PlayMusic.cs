namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PlayMusic
    {
        public uint IDOfMulti; //Guess that's the Multi channel stuff
        public uint WorldID;
        public uint MusicID;

        public PlayMusic(IncomingPacket packet)
        {
            IDOfMulti = packet.ReadUInt();
            WorldID = packet.ReadUInt();
            MusicID = packet.ReadUInt();
        }
    }
}
