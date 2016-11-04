namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PartyChangeMode
    {
        public uint PlayerID;
        public bool SendName;
        public char[] PartyName;

        /// <summary>
        /// Gets called when changing Party from normal to ADV (party level = 10) --> When name is being changed.
        /// </summary>
        public PartyChangeMode(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();                       
            SendName = packet.ReadBool();
            PartyName = packet.ReadString().ToCharArray();
        }
    }
}
