using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SharpFly_Login.Clusters
{
    class ClusterManager : IDisposable
    {
        private List<Cluster> m_Clusters;
        private object m_ListLock;

        public ClusterManager()
        {
            m_Clusters = new List<Cluster>();
            m_ListLock = new object();
        }

        ~ClusterManager()
        {
            lock (m_ListLock)
            {
                foreach (Cluster cluster in m_Clusters)
                    cluster.Dispose();
                m_Clusters.Clear();
            }
        }


        public void AcceptClusters(Socket socket)
        {
            try
            {
                while (true)
                    lock (m_ListLock)
                    {
                        m_Clusters.Add(new Cluster(socket.Accept()));
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Dispose()
        {
            foreach (Cluster cluster in m_Clusters)
                cluster.Dispose();
            m_Clusters.Clear();
        }

        public void ProcessClusters()
        {

            try
            {
                while (true)
                {
                    lock (m_ListLock)
                    {
                        Cluster[] clusters = m_Clusters.ToArray();
                        for (int i = 0; i < clusters.Length; i++)
                            if (clusters[i] != null)
                                clusters[i].ProcessData();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void RemoveCluster(Cluster cluster)
        {
            lock (m_ListLock)
            {
                if (m_Clusters.Contains(cluster))
                    m_Clusters.Remove(cluster);
            }
        }

    }
}