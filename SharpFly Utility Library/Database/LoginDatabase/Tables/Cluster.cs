using System.Data;

namespace SharpFly_Utility_Library.Database.LoginDatabase.Tables
{
    public class Cluster
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public Cluster(DataRow row)
        {
            Id = (uint)row["id"];
            Name = (string)row["name"];
            Password = (string)row["password"];
        }
    }
}
