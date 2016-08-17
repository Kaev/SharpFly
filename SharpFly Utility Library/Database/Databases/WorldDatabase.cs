using SharpFly_Utility_Library.Configuration;

namespace SharpFly_Utility_Library.Database.Databases
{
    public class WorldDatabase : Database
    {
        public WorldDatabase(Config config)
        {
            this.Connection = new MySQLConnector((string)config.GetSetting("MySQLAddress"), (int)config.GetSetting("MySQLPort"), (string)config.GetSetting("MySQLUsername"), (string)config.GetSetting("MySQLPassword"), (string)config.GetSetting("MySQLDatabase"));
            this.Connection.OpenConnection();
        }
    }
}
