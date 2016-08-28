using SharpFly_Packet_Library.Security;
using System;
using System.Collections.Generic;
using System.IO;

namespace SharpFly_Packet_Library.Packets
{
    public class IncomingPacket : PacketBase
    {
        private BinaryReader m_Reader;

        public override byte[] Buffer
        {
            get { return base.Buffer; }
            set
            {
                base.Stream.Seek(0, SeekOrigin.Begin);
                base.Stream.Write(value, 0, value.Length);
            }
        }

        public IncomingPacket()
        {
            base.Stream = new MemoryStream(1452);
            this.m_Reader = new BinaryReader(base.Stream);
        }

        public IncomingPacket(byte[] data)
        {
            base.Stream = new MemoryStream(1452);
            this.m_Reader = new BinaryReader(base.Stream);
            this.Buffer = data;
        }

        public static IncomingPacket[] SplitLoginServerPackets(byte[] buffer)
        {
            int offset = 0;

            List<IncomingPacket> packets = new List<IncomingPacket>();
            BinaryReader reader = new BinaryReader(new MemoryStream(buffer));

            while (true)
            {
                LoginServer.Incoming.PacketHeader header = new LoginServer.Incoming.PacketHeader(reader);
                if (header.Length == 0 || header.StartByte != 0x5E)
                    break;
                int count = (int)LoginServer.Incoming.PacketHeader.Size + header.Length;
                byte[] currentBuffer = new byte[count];
                Array.Copy(buffer, offset, currentBuffer, 0, count);
                IncomingPacket packet = new IncomingPacket(currentBuffer);
                packets.Add(packet);
                offset += count;
            }
            return packets.ToArray();
        }

        public static IncomingPacket[] SplitLoginServerPackets(IncomingPacket packet)
        {
            return SplitLoginServerPackets(packet.Buffer);
        }

        public static IncomingPacket[] SplitClusterServerPackets(byte[] buffer)
        {
            int offset = 0;

            List<IncomingPacket> packets = new List<IncomingPacket>();
            BinaryReader reader = new BinaryReader(new MemoryStream(buffer));

            while (true)
            {
                ClusterServer.Incoming.PacketHeader header = new ClusterServer.Incoming.PacketHeader(reader);
                if (header.Length == 0 || header.StartByte != 0x5E)
                    break;
                int count = (int)ClusterServer.Incoming.PacketHeader.Size + header.Length;
                byte[] currentBuffer = new byte[count];
                Array.Copy(buffer, offset, currentBuffer, 0, count);
                IncomingPacket packet = new IncomingPacket(currentBuffer);
                packets.Add(packet);
                offset += count;
            }
            return packets.ToArray();
        }

        public static IncomingPacket[] SplitClusterServerPackets(IncomingPacket packet)
        {
            return SplitClusterServerPackets(packet.Buffer);
        }

        public bool VerifyHeaders(int sessionKey)
        {
            Crc32 crc = new Crc32();
            long size = this.Size - 13; // Does not include headers
            int calculatedLengthHash = ~(BitConverter.ToInt32(crc.ComputeHash(BitConverter.GetBytes((int)size)), 0) ^ sessionKey);
            byte[] data = new byte[size];
            Array.Copy(this.Buffer, 13, data, 0, size);
            int calculatedDataHash = ~(BitConverter.ToInt32(crc.ComputeHash(data), 0) ^ sessionKey);

            long oldPosition = this.Position;
            this.Position = 0;
            if (ReadByte() != 0x5E)
                return false;

            int lengthHash = ReadInt();
            this.Position += 4;
            int dataHash = ReadInt();
            this.Position = oldPosition;
            return dataHash == calculatedDataHash && lengthHash == calculatedLengthHash;
        }

        public bool ReadBool()
        {
            return m_Reader.ReadBoolean();
        }

        public byte ReadByte()
        {
            return m_Reader.ReadByte();
        }

        public byte[] ReadBytes(int count)
        {
            return m_Reader.ReadBytes(count);
        }

        public float ReadFloat()
        {
            return m_Reader.ReadSingle();
        }

        public int ReadInt()
        {
            return m_Reader.ReadInt32();
        }

        public short ReadShort()
        {
            return m_Reader.ReadInt16();
        }

        public long ReadLong()
        {
            return m_Reader.ReadInt64();
        }

        public uint ReadUInt()
        {
            return m_Reader.ReadUInt32();
        }

        public string ReadString()
        {
            int length = ReadInt();
            byte[] stringBytes = m_Reader.ReadBytes(length);
            string retVal = "";
            for (int i = 0; i < length; i++)
                retVal += (char)stringBytes[i];
            return retVal;
        }
    }
}
