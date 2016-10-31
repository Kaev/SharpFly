using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace SharpFly_Utility_Library.Database.ClusterDatabase.Queries
{
    public class Character
    {
        private static Character m_Instance = null;
        private static readonly object m_Lock = new object();
        private static PreparedStatement m_Query_GetAllCharacters;
        private static PreparedStatement m_Query_GetSingleCharacter;

        public static Character Instance
        {
            get
            {
                lock (m_Lock)
                {
                    if (m_Instance == null)
                        m_Instance = new Character();
                    return m_Instance;
                }
            }
        }

        public void Initialize(Database db)
        {
            m_Query_GetAllCharacters = new PreparedStatement(db, "SELECT * FROM characters");
            m_Query_GetSingleCharacter = new PreparedStatement(db, "SELECT * FROM characters WHERE characterId=@characterId", new MySqlParameter("@characterId", MySqlDbType.Int32));
        }

        public List<Tables.Character> GetAllCharacters()
        {
            List<Tables.Character> clusterList = new List<Tables.Character>();
            DataTable dt = m_Query_GetAllCharacters.Process();
            foreach (DataRow row in dt.Rows)
                clusterList.Add(new Tables.Character(row));
            return clusterList;
        }

        public Tables.Character GetCharacter(uint id)
        {
            DataTable dt = m_Query_GetSingleCharacter.Process(id);
            if (dt.Rows.Count > 0)
                return new Tables.Character(dt.Rows[0]);
            return null;
        }

    }
}
