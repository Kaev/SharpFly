namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    public class GuildMemberCharDeleted
    {
        public uint PlayerID;
        public uint GuildID;

        /// <summary>
        /// Handles what to do when a GuildMember is being deleted: If Master: Also delete Guild and set 2Days waiting time to members Else: Leave guild.
        /// </summary>
        public GuildMemberCharDeleted(IncomingPacket packet)
        {
            PlayerID = packet.ReadUInt();
            GuildID = packet.ReadUInt();
        }
    }
}
