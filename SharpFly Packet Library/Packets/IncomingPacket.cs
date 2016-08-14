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

        public int ReceivedSize { get; set; }

        public IncomingPacket()
        {
            base.Stream = new MemoryStream(1452);
            this.m_Reader = new BinaryReader(base.Stream);
            ReceivedSize = 0;
        }

        public static IncomingPacket[] SplitPackets(byte[] buffer)
        {
            try
            {
                List<IncomingPacket> packets = new List<IncomingPacket>();
                int size = buffer.Length;
                int offset = 0;
                BinaryReader reader = new BinaryReader(new MemoryStream(buffer));

                while (offset < size - 1)
                {
                    reader.BaseStream.Position = offset;
                    reader.ReadByte(); // 0x5E
                    reader.ReadInt32(); // Packet size
                    int currentSize = reader.ReadInt32();
                    if (currentSize == 0)
                        break;

                    int count = 13 + currentSize; // 13 = headers
                    byte[] currentBuffer = new byte[count];
                    Array.Copy(buffer, offset, currentBuffer, 0, count);
                    IncomingPacket packet = new IncomingPacket();
                    packet.Buffer = currentBuffer;
                    packets.Add(packet);
                    offset += count;
                }
                return packets.ToArray();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public static IncomingPacket[] SplitPackets(IncomingPacket packet)
        {
            return SplitPackets(packet.Buffer);
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
            Position = 0;
            if (ReadByte() != 0x5E)
                return false;

            int lengthHash = ReadInt();
            Position += 4;
            int dataHash = ReadInt();
            Position = oldPosition;
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

        public short ReadInt16()
        {
            return m_Reader.ReadInt16();
        }

        public long ReadInt64()
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
