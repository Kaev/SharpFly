using System.IO;

namespace SharpFly_Login.Packets
{
    public abstract class PacketBase
    {
        public virtual byte[] Buffer
        {
            get { return Stream.GetBuffer(); }
            set { }
        }

        public virtual long Size
        {
            get { return Stream.Length; }
        }

        public long Position
        {
            get { return Stream.Position; }
            set { Stream.Position = value;  }
        }

        public MemoryStream Stream { get; set; }
    }
}
