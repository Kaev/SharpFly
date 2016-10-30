using System;
using System.Collections.Generic;
using System.Linq;

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
            Dispose();
        }

        public void AddCluster(Cluster cluster)
        {
            lock (m_ListLock)
            {
                m_Clusters.Add(cluster);

                // Just to be sure the ID between channels and clusters is unique.. will probably change that later
                if (cluster.ClusterData.Id >= Id)
                    Id += cluster.ClusterData.Id * 100 + 1;
            }
        }

        public void Dispose()
        {
            foreach (Cluster cluster in m_Clusters.ToArray())
                cluster.Dispose();
            m_Clusters.Clear();
        }

        public Cluster GetClusterById(uint id)
        {
            return m_Clusters.FirstOrDefault(cluster => cluster.ClusterData.Id == id);
        }

        public List<SharpFly_Packet_Library.Helper.Cluster> GetClusters()
        {
            List<SharpFly_Packet_Library.Helper.Cluster> retVal = new List<SharpFly_Packet_Library.Helper.Cluster>();
            foreach (Cluster cluster in m_Clusters.ToArray())
                retVal.Add(cluster.ClusterData);
            return retVal;
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