using System;
using System.Data;

namespace SharpFly_Utility_Library.Database.LoginDatabase.Tables
{
    public class Account
    {
        public int Id;
        public string Accountname;
        public string Password;
        public bool Banned;
        public bool Verified;

        public Account(DataRow row)
        {
            Id = (int)row["Id"];
            Accountname = (string)row["Accountname"];
            Password = (string)row["Password"];
            Banned = Convert.ToBoolean(row["Banned"]);
            Verified = Convert.ToBoolean(row["Verified"]);
        }
    }
}
