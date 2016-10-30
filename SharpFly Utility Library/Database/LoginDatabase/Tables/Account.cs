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
            Id = (int)row["id"];
            Accountname = (string)row["accountname"];
            Password = (string)row["password"];
            Banned = Convert.ToBoolean(row["banned"]);
            Verified = Convert.ToBoolean(row["verified"]);
        }
    }
}
