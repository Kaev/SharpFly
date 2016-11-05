using SharpFly_Utility_Library.Configuration;
using SharpFly_Utility_Library.Database.ClusterDatabase.Tables;
using System.Collections.Generic;

namespace SharpFly_Utility_Library.Database.ClusterDatabase
{
    public class ClusterDatabase : Database
    {

        public Dictionary<int, Character> Characters;
        public List<CharacterSlot> CharacterSlots;

        public ClusterDatabase(Config config)
        {
            this.Connection = new MySQLConnector((string)config.GetSetting("MySQLAddress"), (int)config.GetSetting("MySQLPort"), (string)config.GetSetting("MySQLUsername"), (string)config.GetSetting("MySQLPassword"), (string)config.GetSetting("MySQLDatabaseCluster"));
            this.Connection.OpenConnection();

            Characters = new Dictionary<int, Character>();
            Queries.Character.Instance.Initialize(this);
            foreach (Character character in Queries.Character.Instance.GetAllCharacters())
                Characters.Add(character.CharacterId, character);

            CharacterSlots = new List<CharacterSlot>();
            Queries.CharacterSlot.Instance.Initialize(this);
            foreach (CharacterSlot slot in Queries.CharacterSlot.Instance.GetAllCharacterSlots())
                CharacterSlots.Add(slot);
        }

        public void AddCharacter(Character character)
        {
            lock("Characters")
            {
                Characters.Add(character.CharacterId, character);
            }
        }

        public void DeleteCharacter(Character character)
        {
            lock("Characters")
            {
                Characters.Remove(character.CharacterId);
            }
        }

        public void AddCharacterSlot(CharacterSlot slot)
        {
            lock ("CharacterSlot")
            {
                CharacterSlots.Add(slot);
            }
        }

        public void DeleteCharacterSlot(CharacterSlot slot)
        {
            lock ("CharacterSlot")
            {
                CharacterSlots.Remove(slot);
            }
        }
    }
}
