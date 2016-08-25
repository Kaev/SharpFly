using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFly_Utility_Library.Database.LoginDatabase.Queries
{
    public class Cluster
    {
        private static Cluster m_Instance = null;
        private static readonly object m_Lock = new object();
        private static PreparedStatement m_Query_GetAllClusters;
        private static PreparedStatement m_Query_GetSingleCluster;

        public static Cluster Instance
        {
            get
            {
                lock (m_Lock)
                {
                    if (m_Instance == null)
                        m_Instance = new Cluster();
                    return m_Instance;
                }
            }
        }

        public void Initialize(Database db)
        {
            m_Query_GetAllClusters = new PreparedStatement(db, "SELECT * FROM clusters");
            m_Query_GetSingleCluster = new PreparedStatement(db, "SELECT * FROM clusters WHERE Id=@id", new MySqlParameter("@id", MySqlDbType.Int32));
        }

        public List<Tables.Cluster> GetAllClusters()
        {
            List<Tables.Cluster> clusterList = new List<Tables.Cluster>();
            DataTable dt = m_Query_GetAllClusters.Process();
            foreach (DataRow row in dt.Rows)
                clusterList.Add(new Tables.Cluster(row));
            return clusterList;
        }

        public Tables.Cluster GetCluster(int id)
        {
            DataTable dt = m_Query_GetSingleCluster.Process(id);
            if (dt.Rows.Count > 0)
                return new Tables.Cluster(dt.Rows[0]);
            return null;
        }
    }
}
