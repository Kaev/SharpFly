using System.Data;

namespace SharpFly_Utility_Library.Database.LoginDatabase.Tables
{
    public class Cluster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }

        public Cluster(DataRow row)
        {
            Id = (int)row["Id"];
            Name = (string)row["Name"];
            Ip = (string)row["Ip"];
        }
    }
}
