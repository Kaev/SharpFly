using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace SharpFly_Utility_Library.Database
{
    public class MySQLConnector
    {
        public MySqlConnection Connection { get; private set; }

        public MySQLConnector(string ip, int port, string username, string password, string database)
        {
            try
            {
                string connectionString = string.Format("server={0};port={1};uid={2};pwd={3};database={4};", ip, port, username, password, database);
                this.Connection = new MySqlConnection(connectionString);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool CheckConnection()
        {
            if (Connection.State == ConnectionState.Open)
                return true;
            return false;
        }

        public void CloseConnection()
        {
            if (Connection.State == ConnectionState.Open)
                Connection.Close();
        }

        public void OpenConnection()
        {
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
        }
    }
}
