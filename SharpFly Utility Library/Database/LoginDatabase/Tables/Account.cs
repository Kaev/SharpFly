using System;
using System.Data;

namespace SharpFly_Utility_Library.Database.LoginDatabase.Tables
{
    public class Account
    {
        public int Id { get; set; }
        public string Accountname { get; set; }
        public string Password { get; set; }
        public bool Banned { get; set; }
        public bool Verified { get; set; }

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
