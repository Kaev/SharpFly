namespace SharpFly_Utility_Library.Configuration
{
    public class WorldServerConfig : Config
    {
        public WorldServerConfig(string path) : base(path)
        {
            base.Read("MySQLAddress", "Address", "MySQL", typeof(string));
            base.Read("MySQLUsername", "Username", "MySQL", typeof(string));
            base.Read("MySQLPassword", "Password", "MySQL", typeof(string));
            base.Read("MySQLDatabaseLogin", "DatabaseLogin", "MySQL", typeof(string));
            base.Read("MySQLDatabaseCluster", "DatabaseCluster", "MySQL", typeof(string));
            base.Read("MySQLPort", "Port", "MySQL", typeof(int));
            base.Read("ClusterAuthorizationPassword", "ClusterAuthorizationPassword", "Security", typeof(string));
            base.Read("Address", "Address", "World", typeof(string));
            base.Read("ClusterAddress", "ClusterAddress", "World", typeof(string));
            base.Read("ClusterPort", "ClusterPort", "World", typeof(int));
            base.Read("WorldStartPort", "WorldStartPort", "World", typeof(int));
            base.Read("ChannelName", "ChannelName", "World", typeof(string));
        }
    }
}
