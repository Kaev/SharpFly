using MySql.Data.MySqlClient;
using System;
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
        private static PreparedStatement m_Query_GetHighestCharacterId;
        private static PreparedStatement m_Query_InsertCharacter;

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
            m_Query_GetAllCharacters = new PreparedStatement(db, "SELECT * FROM `character`");
            m_Query_GetSingleCharacter = new PreparedStatement(db, "SELECT * FROM `character` WHERE characterId=@characterId", new MySqlParameter("@characterId", MySqlDbType.Int32));
            m_Query_GetHighestCharacterId = new PreparedStatement(db, "SELECT MAX(characterId) AS `highestCharId` FROM `character`");
            m_Query_InsertCharacter = new PreparedStatement(db, "INSERT INTO `character`(name, clusterId, classId, skinset, hairStyle, hairColor, headMesh, face, gender, strength, stamina, dexterity, intelligence, skillpoints, statpoints, level, exp, map, x, y, z, orientation, penya, flyingLevel, flyingExp, hp, mp, size, pvpPoints, pkPoints, guildId, bag1TimeLeft, bag2Timeleft, msgState, motionFlags, movementFlags, playerFlags) VALUES (@name, @clusterId, @classId, @skinset, @hairStyle, @hairColor, @headMesh, @face, @gender, @strength, @stamina, @dexterity, @intelligence, @skillpoints, @statpoints, @level, @exp, @map, @x, @y, @z, @orientation, @penya, @flyingLevel, @flyingExp, @hp, @mp, @size, @pvpPoints, @pkPoints, @guildId, @bag1TimeLeft, @bag2Timeleft, @msgState, @motionFlags, @movementFlags, @playerFlags)",
                new MySqlParameter("@name", MySqlDbType.VarChar),
                new MySqlParameter("@clusterId", MySqlDbType.UInt32),
                new MySqlParameter("@classId", MySqlDbType.Int32),
                new MySqlParameter("@skinset", MySqlDbType.UInt32),
                new MySqlParameter("@hairStyle", MySqlDbType.UInt32),
                new MySqlParameter("@hairColor", MySqlDbType.UInt32),
                new MySqlParameter("@headMesh", MySqlDbType.UInt32),
                new MySqlParameter("@face", MySqlDbType.UInt32),
                new MySqlParameter("@gender", MySqlDbType.Byte),
                new MySqlParameter("@strength", MySqlDbType.UInt32),
                new MySqlParameter("@stamina", MySqlDbType.Int32),
                new MySqlParameter("@dexterity", MySqlDbType.UInt32),
                new MySqlParameter("@intelligence", MySqlDbType.UInt32),
                new MySqlParameter("@skillpoints", MySqlDbType.UInt32),
                new MySqlParameter("@statpoints", MySqlDbType.UInt32),
                new MySqlParameter("@level", MySqlDbType.Int32),
                new MySqlParameter("@exp", MySqlDbType.UInt32),
                new MySqlParameter("@map", MySqlDbType.UInt32),
                new MySqlParameter("@x", MySqlDbType.Float),
                new MySqlParameter("@y", MySqlDbType.Float),
                new MySqlParameter("@z", MySqlDbType.Float),
                new MySqlParameter("@orientation", MySqlDbType.Float),
                new MySqlParameter("@penya", MySqlDbType.UInt64),
                new MySqlParameter("@flyingLevel", MySqlDbType.Int32),
                new MySqlParameter("@flyingExp", MySqlDbType.Int32),
                new MySqlParameter("@hp", MySqlDbType.UInt32),
                new MySqlParameter("@mp", MySqlDbType.UInt32),
                new MySqlParameter("@size", MySqlDbType.UInt32),
                new MySqlParameter("@pvpPoints", MySqlDbType.Int32),
                new MySqlParameter("@pkPoints", MySqlDbType.Int32),
                new MySqlParameter("@guildId", MySqlDbType.UInt32),
                new MySqlParameter("@bag1TimeLeft", MySqlDbType.UInt32),
                new MySqlParameter("@bag2Timeleft", MySqlDbType.UInt32),
                new MySqlParameter("@msgState", MySqlDbType.UInt32),
                new MySqlParameter("@motionFlags", MySqlDbType.UInt32),
                new MySqlParameter("@movementFlags", MySqlDbType.UInt32),
                new MySqlParameter("@playerFlags", MySqlDbType.UInt32)
            );
        }

        public void AddCharacter(Tables.Character character)
        {
            m_Query_InsertCharacter.Process(character.Name, character.ClusterId, character.ClassId, character.Skinset, character.HairStyle, character.HairColor, character.HeadMesh, character.Face, character.Gender, character.Strength, character.Stamina, character.Dexterity, character.Intelligence, character.SkillPoints, character.StatPoints, character.Level, character.Experience, character.Map, character.Position.X, character.Position.Y, character.Position.Z, character.Position.O, character.Penya, character.FlyingLevel, character.FlyingExp, character.HP, character.MP, character.Size, character.PvPPoints, character.PKPoints, character.GuildId, character.Bag1TimeLeft, character.Bag2TimeLeft, character.MsgState, character.MotionFlags, character.MovementFlags, character.PlayerFlags);
        }

        public List<Tables.Character> GetAllCharacters()
        {
            List<Tables.Character> clusterList = new List<Tables.Character>();
            DataTable dt = m_Query_GetAllCharacters.Process();
            if (dt != null)
                foreach (DataRow row in dt.Rows)
                    clusterList.Add(new Tables.Character(row));
            return clusterList;
        }

        public Tables.Character GetCharacter(uint id)
        {
            DataTable dt = m_Query_GetSingleCharacter.Process(id);
            if (dt != null && dt.Rows.Count > 0)
                return new Tables.Character(dt.Rows[0]);
            return null;
        }

        public int GetNewCharacterId()
        {
            DataTable dt = m_Query_GetHighestCharacterId.Process();
            if (dt != null && dt.Rows.Count > 0)
            {
                object value = dt.Rows[0]["highestCharId"];
                if (!Convert.IsDBNull(value))
                    return Convert.ToInt32(value) + 1;
            }  
            return 1;
        }

    }
}
