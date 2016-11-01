using System.Data;

namespace SharpFly_Utility_Library.Database.ClusterDatabase.Tables
{
    class CharacterSlot
    {
        public int CharacterId { get; set; }
        public int SlotId { get; set; }
        public int accountId { get; set; }

        public CharacterSlot(DataRow row)
        {
            CharacterId = (int)row["characterId"];
            SlotId = (int)row["slotId"];
            accountId = (int)row["accountId"];
        }
    }
}
