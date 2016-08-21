using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SharpFly_World.Player
{
    public class PlayerManager : IDisposable
    {
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
                lock (m_ListLock)
                {
                    Player[] players = m_Players.ToArray();
                    for (int i = 0; i < players.Length; i++)
                        if (players[i] != null)
                            players[i].ProcessData();
                }
            }
        }

        public void RemovePlayer(Player player)
        {
            lock (m_ListLock)
            {
                if (m_Players.Contains(player))
                    m_Players.Remove(player);
            }
        }
    }
}