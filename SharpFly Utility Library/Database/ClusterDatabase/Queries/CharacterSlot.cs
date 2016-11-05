using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace SharpFly_Utility_Library.Database.ClusterDatabase.Queries
{
    public class CharacterSlot
    {
        private static CharacterSlot m_Instance = null;
        private static readonly object m_Lock = new object();
        private static PreparedStatement m_Query_GetAllCharacterSlots;
        private static PreparedStatement m_Query_GetAllCharacterSlotsForAccount;
        private static PreparedStatement m_Query_InsertCharacterSlot;
        private static PreparedStatement m_Query_GetSingleCharacterSlotForAccount;
        private static PreparedStatement m_Query_DeleteCharacterSlot;

        public static CharacterSlot Instance
        {
            get
            {
                lock (m_Lock)
                {
                    if (m_Instance == null)
                        m_Instance = new CharacterSlot();
                    return m_Instance;
                }
            }
        }

        public void Initialize(Database db)
        {
            m_Query_GetAllCharacterSlots = new PreparedStatement(db, "SELECT * FROM `character_slot`");
            m_Query_GetAllCharacterSlotsForAccount = new PreparedStatement(db, "SELECT * FROM `character_slot` WHERE accountid=@accountId", new MySqlParameter("@accountId", MySqlDbType.Int32));
            m_Query_InsertCharacterSlot = new PreparedStatement(db, "INSERT INTO character_slot(characterId, slotId, accountId) VALUES (@characterId, @slotId, @accountId)", new MySqlParameter("@characterId", MySqlDbType.Int32), new MySqlParameter("@slotId", MySqlDbType.Int32), new MySqlParameter("@accountId", MySqlDbType.Int32));
            m_Query_GetSingleCharacterSlotForAccount = new PreparedStatement(db, "SELECT * FROM character_slots WHERE accountid=@accountId AND slotId=@slotId", new MySqlParameter("@accountId", MySqlDbType.Int32), new MySqlParameter("@slotId", MySqlDbType.Int32));
            m_Query_DeleteCharacterSlot = new PreparedStatement(db, "DELETE FROM `character_slot` WHERE characterId=@characterId AND accountId=@accountId", new MySqlParameter("@characterId", MySqlDbType.Int32), new MySqlParameter("@accountId", MySqlDbType.Int32));
        }

        public List<Tables.CharacterSlot> GetAllCharacterSlots()
        {
            List<Tables.CharacterSlot> slotList = new List<Tables.CharacterSlot>();
            DataTable dt = m_Query_GetAllCharacterSlots.Process();
            if (dt != null)
                foreach (DataRow row in dt.Rows)
                    slotList.Add(new Tables.CharacterSlot(row));
            return slotList;
        }

        public List<Tables.CharacterSlot> GetAllCharacterSlotsForAccount(uint accountId)
        {
            List<Tables.CharacterSlot> slotList = new List<Tables.CharacterSlot>();
            DataTable dt = m_Query_GetAllCharacterSlotsForAccount.Process(accountId);
            if (dt != null)
                foreach (DataRow row in dt.Rows)
                    slotList.Add(new Tables.CharacterSlot(row));
            return slotList;
        }

        public void AddCharacterSlot(Tables.CharacterSlot slot)
        {
            m_Query_InsertCharacterSlot.Process(slot.CharacterId, slot.SlotId, slot.AccountId);
        }

        public Tables.CharacterSlot GetSingleCharacterSlotForAccount(uint accountId, uint slotId)
        {
            DataTable dt = m_Query_GetSingleCharacterSlotForAccount.Process(accountId, slotId);
            if (dt != null && dt.Rows.Count > 0)
                return new Tables.CharacterSlot(dt.Rows[0]);
            return null;
        }

        public void DeleteCharacterSlot(Tables.CharacterSlot slot)
        {
            m_Query_DeleteCharacterSlot.Process(slot.CharacterId, slot.AccountId);
        }
    }
}
