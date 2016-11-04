using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SharpFly_Cluster.Player
{
    public class PlayerManager : IDisposable
    {
        private Player[] m_PlayerProcessing;
        private List<Player> m_Players;
        private object m_ListLock;

        public PlayerManager()
        {
            m_Players = new List<Player>();
            m_ListLock = new object();
        }

        public void AcceptPlayers(Socket socket)
        {
            while (true)
                lock (m_ListLock)
                {
                    m_Players.Add(new Player(socket.Accept()));
                    m_PlayerProcessing = m_Players.ToArray();
                }
        }

        public void Dispose()
        {
            foreach (Player player in m_Players)
                player.Dispose();
            m_Players.Clear();
        }

        public void ProcessPlayers()
        {
            while (true)
            {
                if (m_PlayerProcessing != null)
                    for (int i = 0; i < m_PlayerProcessing.Length; i++)
                        if (m_PlayerProcessing[i] != null)
                            m_PlayerProcessing[i].ProcessData();
            }
        }

        public void RemovePlayer(Player player)
        {
            lock (m_ListLock)
            {
                if (m_Players.Contains(player))
                    m_Players.Remove(player);
                m_PlayerProcessing = m_Players.ToArray();
            }
        }
    }
}