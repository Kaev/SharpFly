using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SharpFly_Login.Clients
{
    class ClientManager
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
            try
            {
                while (true)
                    lock (m_ListLock)
                    {
                        m_Clients.Add(new Client(socket.Accept()));
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ProcessUsers()
        {

            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void RemoveUser(Client client)
        {
            if (m_Clients.Contains(client))
                m_Clients.Remove(client);
        }

    }
}
