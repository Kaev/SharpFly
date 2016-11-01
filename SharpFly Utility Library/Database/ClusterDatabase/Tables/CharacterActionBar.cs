using System.Data;

namespace SharpFly_Utility_Library.Database.ClusterDatabase.Tables
{
    class CharacterActionBar
    {
        public string ActionSlotSkillId { get; set; }
        public string ActionSlotOption { get; set; }
        public int ActionBarProgress { get; set; }

        public CharacterActionBar(DataRow row)
        {
            ActionSlotSkillId = (string)row["actionSlotSkillId"];
            ActionSlotOption = (string)row["actionSlotOption"];
            ActionBarProgress = (int)row["actionBarProgress"];
        }
    }
}
