using System.IO;
using System.Net.Sockets;
using System;

namespace SharpFly_Packet_Library.Packets
{
    class OutgoingPacket : PacketBase
    {
        private BinaryWriter m_Writer { get; set; }

        public OutgoingPacket()
        {
            this.MergedPacketCount = 0;
            this.Stream = new MemoryStream();
            this.m_Writer = new BinaryWriter(base.Stream);
            Write((byte)0x5E); // Packets always start with 0x5E
            Write(0); // Packet size
        }

        public OutgoingPacket(uint header)
        {
            this.MergedPacketCount = 0;
            this.Stream = new MemoryStream();
            this.m_Writer = new BinaryWriter(base.Stream);
            Write((byte)0x5E); // Packets always start with 0x5E
            Write(0); // Packet size
            Write(header);
        }

        public override byte[] Buffer
        {
            get { return base.Stream.ToArray(); }
        }

        public int MergedPacketCount { get; set; }

        public override long Size
        { get { return base.Size - 5; } }

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

        public void Write (uint value)
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
            int length = value.Length;
            Write(length);
            for(int i = 0; i < length; i++)
                m_Writer.Write((byte)value[i]);
        }

        public void Write(params byte[] values)
        {
            m_Writer.Write(values);
        }

        public void Send(Socket socket)
        {
            try
            {
                long oldPosition = Position;
                Position = 1;
                Write((int)Size);
                Position = oldPosition;
                if (socket.Connected)
                    socket.Send(Buffer);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void StartMergedPacket(int moverId, uint command)
        {
            StartMergedPacket(moverId, command, 0xFFFFFF00);
        }

        public void StartMergedPacket(int moverId, uint command, uint mainCommand)
        {
            if(MergedPacketCount == 0)
            {
                Write(mainCommand);
                Write(0);
                Write((short)1);
                MergedPacketCount++;
            }
            else
            {
                if(moverId != -1)
                {
                    Stream.Seek(13, SeekOrigin.Begin);
                    Write((short)MergedPacketCount++);
                    Stream.Seek(0, SeekOrigin.End);
                }
                Write(moverId);
                Write((short)command);
            }
        }
    }
}
