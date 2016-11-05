using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace SharpFly_Utility_Library.Database.LoginDatabase.Queries
{
    public class Account
    {
        private static Account m_Instance = null;
        private static readonly object m_Lock = new object();
        private static PreparedStatement m_Query_GetAllAccounts;
        private static PreparedStatement m_Query_GetSingleAccount;

        private Account() { }

        public static Account Instance
        {
            get
            {
                lock (m_Lock)
                {
                    if (m_Instance == null)
                        m_Instance = new Account();
                    return m_Instance;
                }
            }
        }

        public void Initialize(Database db)
        {
            m_Query_GetAllAccounts = new PreparedStatement(db, "SELECT * FROM `accounts`");
            m_Query_GetSingleAccount = new PreparedStatement(db, "SELECT * FROM `accounts` WHERE accountname=@accountname", new MySqlParameter("@accountname", MySqlDbType.VarChar));
        }

        public List<Tables.Account> GetAllAccounts()
        {
            List<Tables.Account> accountList = new List<Tables.Account>();
            DataTable dt = m_Query_GetAllAccounts.Process();
            if (dt != null)
                foreach (DataRow row in dt.Rows)
                    accountList.Add(new Tables.Account(row));
            return accountList;
        }

        public Tables.Account GetAccount(string accountName)
        {
            DataTable dt = m_Query_GetSingleAccount.Process(accountName);
            if (dt.Rows.Count > 0)
                return new Tables.Account(dt.Rows[0]);
            return null;
        }
    }
}
