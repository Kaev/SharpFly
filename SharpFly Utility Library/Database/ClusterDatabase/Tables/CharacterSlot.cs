using System;
using System.Data;

namespace SharpFly_Utility_Library.Database.ClusterDatabase.Tables
{
    public class CharacterSlot
    {
        public int CharacterId { get; set; }
        public int SlotId { get; set; }
        public uint AccountId { get; set; }

        public CharacterSlot() { }

        public CharacterSlot(DataRow row)
        {
            CharacterId = Convert.ToInt32(row["characterId"]);
            SlotId = Convert.ToInt32(row["slotId"]);
            AccountId = Convert.ToUInt32(row["accountId"]);
        }
    }
}
