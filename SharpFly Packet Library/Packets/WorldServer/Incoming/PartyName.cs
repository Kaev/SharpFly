namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    class PartyName
    {
        public uint Size { get; private set; }         
        public uint PlayerID { get; private set; }     
        public char[] partyName { get; private set; }   

        public PartyName(IncomingPacket packet)
        {
            Size = packet.ReadUInt();
            PlayerID = packet.ReadUInt();
            partyName = packet.ReadString().ToCharArray();
        }
    }
}
