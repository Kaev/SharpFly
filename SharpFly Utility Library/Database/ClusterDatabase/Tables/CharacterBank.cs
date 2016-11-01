using System.Data;

namespace SharpFly_Utility_Library.Database.ClusterDatabase.Tables
{
    class CharacterBank
    {
        public int Bank0Penya { get; set; }
        public int Bank1Penya { get; set; }
        public int Bank2Penya { get; set; }
        public string BankPassword { get; set; }

        public CharacterBank(DataRow row)
        {
            Bank0Penya = (int)row["bank0Penya"];
            Bank1Penya = (int)row["bank1Penya"];
            Bank2Penya = (int)row["bank2Penya"];
            BankPassword = (string)row["bankPassword"];
        }
    }
}
