using SharpFly_Utility_Library.Configuration;

namespace SharpFly_Utility_Library.Database.ClusterDatabase
{
    public class ClusterDatabase : Database
    {
        public ClusterDatabase(Config config)
        {
            this.Connection = new MySQLConnector((string)config.GetSetting("MySQLAddress"), (int)config.GetSetting("MySQLPort"), (string)config.GetSetting("MySQLUsername"), (string)config.GetSetting("MySQLPassword"), (string)config.GetSetting("MySQLDatabaseCluster"));
            this.Connection.OpenConnection();
        }
    }
}
