namespace SharpFly_Packet_Library.Packets.WorldServer.Incoming
{
    class PrivateMessageManager
    {
        public uint TargetPlayerID;
        public string Message;

        /// <summary>
        /// Receives packet and checks State of friend (TAG_OK or TAG_BLOCKED) and sends or blocks the message accordingly.
        /// </summary>
        public  PrivateMessageManager(IncomingPacket packet)
        {
            TargetPlayerID = packet.ReadUInt();
            Message = packet.ReadString();
        }
    }
}
