namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PartyChangeName
    {
        public uint PlayerID;
        public string PartyName;

        /// <summary>
        /// Gets called when party name is being changed.
        /// </summary>
        public PartyChangeName(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            PartyName = packet.ReadString();
        }
    }
}
