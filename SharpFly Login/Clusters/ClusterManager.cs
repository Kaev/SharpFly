using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SharpFly_Login.Clusters
{
    public class ClusterManager : IDisposable
    {
        private List<Cluster> m_Clusters;
        private object m_ListLock;

        public static uint Id = 0;

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
            while (true)
                lock (m_ListLock)
                {
                    m_Clusters.Add(new Cluster(socket.Accept()));
                }
        }

        public void Dispose()
        {
            foreach (Cluster cluster in m_Clusters)
                cluster.Dispose();
            m_Clusters.Clear();
        }

        public List<SharpFly_Packet_Library.Helper.Cluster> GetClusters()
        {
            List<SharpFly_Packet_Library.Helper.Cluster> retVal = new List<SharpFly_Packet_Library.Helper.Cluster>();
            foreach (Cluster cluster in m_Clusters)
                retVal.Add(cluster.ClusterData);
            return retVal;
        }

        public void ProcessClusters()
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