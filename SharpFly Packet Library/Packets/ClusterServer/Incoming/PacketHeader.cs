using System;
using System.IO;

namespace SharpFly_Packet_Library.Packets.ClusterServer.Incoming
{
    public class PacketHeader
    {
        // Header data
        public byte StartByte { get; private set; }
        public int LengthHash { get; private set; }
        public int Length { get; private set; }
        public int DataHash { get; private set; }
        public byte[] Data { get; private set; }

        public static uint Size = 13;
        public static uint DataStartPosition = 17;

        public PacketHeader(BinaryReader reader)
        {
            StartByte = reader.ReadByte();

            // This happens right after the ping packet.. where does this data come from? Can't even find it via Wireshark..
            if (StartByte != 0x5E)
                return;

            LengthHash = reader.ReadInt32();
            Length = reader.ReadInt32();

            if (Length > reader.BaseStream.Length - Size)
            {
                Length = 0;
                return;
            }

            DataHash = reader.ReadInt32();
            //reader.ReadInt32(); // Always -1, skip it to get the actual data (for now, could imagine it's something like a object id)
            Data = reader.ReadBytes(Length);
        }
    }
}
