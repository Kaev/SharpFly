using MySql.Data.MySqlClient;

namespace SharpFly_Login.Database
{
    public static class PreparedStatements
    {
        public static PreparedStatement GETACCOUNTINFORMATIONS = new PreparedStatement("SELECT * FROM accounts WHERE Accountname=@accountname", new MySqlParameter("@accountname", MySqlDbType.VarChar));
    }
}
