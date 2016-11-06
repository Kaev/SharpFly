using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpFly_Cluster.Channel
{
    public class ChannelManager : IDisposable
    {
        private List<Channel> m_Channels;
        private object m_ListLock;

        public static uint Id = 0;

        public ChannelManager()
        {
            m_Channels = new List<Channel>();
            m_ListLock = new object();
        }

        ~ChannelManager()
        {
            Dispose();
        }

        public void AddChannel(Channel channel)
        {
            lock (m_ListLock)
            {
                m_Channels.Add(channel);
            }
        }

        public void Dispose()
        {
            foreach (Channel channel in m_Channels.ToArray())
                channel.Dispose();
            m_Channels.Clear();
        }

        public Channel GetChannelById(uint id)
        {
            return m_Channels.FirstOrDefault(channel => channel.ChannelData.Id == id);
        }

        public List<SharpFly_Packet_Library.Helper.Channel> GetChannels()
        {
            List<SharpFly_Packet_Library.Helper.Channel> retVal = new List<SharpFly_Packet_Library.Helper.Channel>();
            foreach (Channel channel in m_Channels.ToArray())
                retVal.Add(channel.ChannelData);
            return retVal;
        }

        public void RemoveChannel(Channel channel)
        {
            lock (m_ListLock)
            {
                if (m_Channels.Contains(channel))
                    m_Channels.Remove(channel);
            }
        }
    }
}
