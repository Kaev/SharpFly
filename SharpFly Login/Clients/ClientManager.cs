using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SharpFly_Login.Clients
{
    public class ClientManager : IDisposable
    {
        private List<Client> m_Clients;
        private object m_ListLock;

        public ClientManager()
        {
            m_Clients = new List<Client>();
            m_ListLock = new object();
        }

        public void AcceptUsers(Socket socket)
        {
            while (true)
                lock (m_ListLock)
                {
                    m_Clients.Add(new Client(socket.Accept()));
                }
        }

        public void Dispose()
        {
            foreach (Client client in m_Clients)
                client.Dispose();
            m_Clients.Clear();
        }

        public void ProcessUsers()
        {
            while (true)
            {
                lock (m_ListLock)
                {
                    Client[] clients = m_Clients.ToArray();
                    for (int i = 0; i < clients.Length; i++)
                        if (clients[i] != null)
                            clients[i].ProcessData();
                }
            }
        }

        public void RemoveUser(Client client)
        {
            lock (m_ListLock)
            {
                if (m_Clients.Contains(client))
                    m_Clients.Remove(client);
            }
        }

    }
}
