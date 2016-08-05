using System;
using System.Data;
using SharpFly_Login.Utility;

namespace SharpFly_Login.Database
{
    static class MySQL
    {
        public static MySql.Data.MySqlClient.MySqlConnection Connection { get; private set; }

        static MySQL()
        {
            string connectionString = string.Format("server={0};port={1};uid={2};pwd={3};database={4};", Config.MysqlAddress, Config.MysqlPort, Config.MysqlUsername, Config.MysqlPassword, Config.MysqlDatabase);
            Connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

            try
            {
                OpenConnection();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static bool CheckConnection()
        {
            if (Connection.State == ConnectionState.Open)
                return true;
            return false;
        }

        public static void CloseConnection()
        {
            if (Connection.State == ConnectionState.Open)
                Connection.Close();
        }

        public static void OpenConnection()
        {
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
        }
    }
}
