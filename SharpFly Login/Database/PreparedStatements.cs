using MySql.Data.MySqlClient;
using SharpFly_Login.Server;
using SharpFly_Utility_Library.Database;

namespace SharpFly_Login.Database
{
    public static class PreparedStatements
    {
        public static PreparedStatement GET_ACCOUNT_INFORMATIONS = new PreparedStatement(LoginServer.LoginDatabase, "SELECT * FROM accounts WHERE Accountname=@accountname", new MySqlParameter("@accountname", MySqlDbType.VarChar));
    }
}
