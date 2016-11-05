namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class PartyChangeName
    {
        public uint PlayerID;
        public string PartyName;

        /// <summary>
        /// Difference PartySendName and PartyChangeName: PartySendName is being used when sending Information to the WorldServer 
        /// where PartyChangeName is being used when Changing the party name (converting to advanced Party).
        /// </summary>
        public PartyChangeName(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            PartyName = packet.ReadString();
        }
    }
}
