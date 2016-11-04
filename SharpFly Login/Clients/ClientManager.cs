using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SharpFly_Login.Clients
{
    public class ClientManager : IDisposable
    {
        private Client[] m_ClientsProcessing;
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
                    m_ClientsProcessing = m_Clients.ToArray();
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
                if (m_ClientsProcessing != null)
                    for (int i = 0; i < m_ClientsProcessing.Length; i++)
                        if (m_ClientsProcessing[i] != null)
                            m_ClientsProcessing[i].ProcessData();
            }
        }

        public void RemoveUser(Client client)
        {
            lock (m_ListLock)
            {
                if (m_Clients.Contains(client))
                    m_Clients.Remove(client);
                m_ClientsProcessing = m_Clients.ToArray();
            }
        }

    }
}
