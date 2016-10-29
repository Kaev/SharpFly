using NetMQ;
using NetMQ.Sockets;
using System.IO;

namespace SharpFly_Packet_Library.Packets
{
    class OutgoingInterserverPacket : PacketBase
    {
        private BinaryWriter m_Writer { get; set; }

        public OutgoingInterserverPacket()
        {
            this.Stream = new MemoryStream();
            this.m_Writer = new BinaryWriter(base.Stream);
        }

        public OutgoingInterserverPacket(uint header)
        {
            this.Stream = new MemoryStream();
            this.m_Writer = new BinaryWriter(base.Stream);
            Write(header);
        }

        public override byte[] Buffer
        {
            get { return base.Stream.ToArray(); }
        }

        public override long Size
        { get { return base.Size; } }

        public void Write(bool value)
        {
            m_Writer.Write(value);
        }

        public void Write(byte value)
        {
            m_Writer.Write(value);
        }

        public void Write(int value)
        {
            m_Writer.Write(value);
        }

        public void Write(uint value)
        {
            m_Writer.Write(value);
        }

        public void Write(float value)
        {
            m_Writer.Write(value);
        }

        public void Write(short value)
        {
            m_Writer.Write(value);
        }

        public void Write(long value)
        {
            m_Writer.Write(value);
        }

        public void Write(string value)
        {
            m_Writer.Write(value);
        }

        public void Write(params byte[] values)
        {
            m_Writer.Write(values);
        }

        public void Send(PushSocket socket)
        {
            socket.SendFrame(Buffer);
        }
    }
}
