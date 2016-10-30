using SharpFly_Utility_Library.Configuration;
using SharpFly_Utility_Library.Database.LoginDatabase.Tables;
using System.Collections.Generic;

namespace SharpFly_Utility_Library.Database.LoginDatabase
{
    public class LoginDatabase : Database
    {

        public Dictionary<string, Account> Accounts;
        public Dictionary<string, Cluster> Cluster;

        public LoginDatabase(Config config)
        {
            this.Connection = new MySQLConnector((string)config.GetSetting("MySQLAddress"), (int)config.GetSetting("MySQLPort"), (string)config.GetSetting("MySQLUsername"), (string)config.GetSetting("MySQLPassword"), (string)config.GetSetting("MySQLDatabaseLogin"));
            this.Connection.OpenConnection();

            Accounts = new Dictionary<string, Account>();
            Queries.Account.Instance.Initialize(this);
            foreach (Account account in Queries.Account.Instance.GetAllAccounts())
                Accounts.Add(account.Accountname, account);

            Cluster = new Dictionary<string, Cluster>();
            Queries.Cluster.Instance.Initialize(this);
            foreach (Cluster cluster in Queries.Cluster.Instance.GetAllClusters())
                Cluster.Add(cluster.Name, cluster);
        }
    }
}
