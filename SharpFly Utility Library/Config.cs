using System;

namespace SharpFly_Utility_Library
{
    public static class Config
    {
        public static string ClientBuildDate { get; set; }
        public static string Md5Salt { get; set; }
        public static string MysqlAddress { get; set; }
        public static string MysqlDatabase { get; set; }
        public static string MysqlUsername { get; set; }
        public static string MysqlPassword { get; set; }
        public static int MysqlPort { get; set; }

        public static bool ReadConfig()
        {
            try
            {
                IniFile file = new IniFile("Resources/Config/Login.ini");
                if (file.Path != "")
                {
                    ClientBuildDate = file.Read("ClientBuildDate", "Security");
                    Md5Salt = file.Read("Md5Salt", "Security");
                    MysqlAddress = file.Read("Address", "Mysql");
                    MysqlDatabase = file.Read("Database", "Mysql");
                    MysqlUsername = file.Read("Username", "Mysql");
                    MysqlPassword = file.Read("Password", "Mysql");
                    MysqlPort = int.Parse(file.Read("Port", "Mysql"));
                    return true;
                }
                return false;
            }
            catch
            {
                Console.WriteLine("Please check your configuration file!");
                return false;
            }
        }
    }
}
