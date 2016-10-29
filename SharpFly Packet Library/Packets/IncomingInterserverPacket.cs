using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFly_Packet_Library.Packets
{
    public class IncomingInterserverPacket : PacketBase
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

        public IncomingInterserverPacket()
        {
            base.Stream = new MemoryStream(1452);
            this.m_Reader = new BinaryReader(base.Stream);
        }

        public IncomingInterserverPacket(byte[] data)
        {
            base.Stream = new MemoryStream(1452);
            this.m_Reader = new BinaryReader(base.Stream);
            this.Buffer = data;
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
            return m_Reader.ReadString();
        }
    }
}
