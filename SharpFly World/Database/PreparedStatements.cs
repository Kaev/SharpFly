using MySql.Data.MySqlClient;
using SharpFly_Utility_Library.Database;
using SharpFly_World.Server;

namespace SharpFly_World.Database
{
    public static class PreparedStatements
    {
        public static PreparedStatement GET_ACCOUNT_INFORMATIONS = new PreparedStatement(WorldServer.LoginDatabase, "SELECT * FROM accounts WHERE Accountname=@accountname", new MySqlParameter("@accountname", MySqlDbType.VarChar));
    }
}
