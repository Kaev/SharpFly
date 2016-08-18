namespace SharpFly_Utility_Library.Configuration
{
    public class WorldServerConfig : Config
    {
        public WorldServerConfig(string path) : base(path)
        {
            base.Read("MySQLAddress", "Address", "MySQL", typeof(string));
            base.Read("MySQLUsername", "Username", "MySQL", typeof(string));
            base.Read("MySQLPassword", "Password", "MySQL", typeof(string));
            base.Read("MySQLDatabase", "Database", "MySQL", typeof(string));
            base.Read("MySQLPort", "Port", "MySQL", typeof(int));
            base.Read("ClusterAuthorizationPassword", "ClusterAuthorizationPassword", "Security", typeof(string));
        }
    }
}
