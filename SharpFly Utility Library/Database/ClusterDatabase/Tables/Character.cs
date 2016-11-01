using SharpFly_Utility_Library.Math;
using System.Data;

namespace SharpFly_Utility_Library.Database.ClusterDatabase.Tables
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int ClusterId { get; set; }
        public int ClassId { get; set; }
        public int HairStyle { get; set; }
        public int HairColor { get; set; }
        public int Face { get; set; }
        public int Gender { get; set; }
        public int Strength { get; set; }
        public int Stamina { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int SkillPoints { get; set; }
        public int StatPoints { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int Map { get; set; }
        public Vector4<double> Position { get; set; }
        public int Penya { get; set; }
        public int FlyingLevel { get; set; }
        public int FlyingExp { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int Size { get; set; }
        public int PvPPoints { get; set; }
        public int PKPoints { get; set; }
        public int GuildId { get; set; }
        public int Bag1TimeLeft { get; set; }
        public int Bag2TimeLeft { get; set; }
        public int MsgState { get; set; }
        public int MotionFlags { get; set; }
        public int MovementFlags { get; set; }
        public int PlayerFlags { get; set; }

        public Character(DataRow row)
        {
            CharacterId = (int)row["characterId"];
            Name = (string)row["name"];
            ClusterId = (int)row["clusterId"];
            ClassId = (int)row["classId"];
            HairStyle = (int)row["hairStyle"];
            HairColor = (int)row["hairColor"];
            Face = (int)row["face"];
            Gender = (int)row["gender"];
            Strength = (int)row["strength"];
            Stamina = (int)row["stamina"];
            Dexterity = (int)row["dexterity"];
            Intelligence = (int)row["intelligence"];
            SkillPoints = (int)row["skillpoints"];
            StatPoints = (int)row["statpoints"];
            Level = (int)row["level"];
            Experience = (int)row["exp"];
            Map = (int)row["map"];
            Position = new Vector4<double>((double)row["x"], (double)row["y"], (double)row["z"], (double)row["orientation"]);
            Penya = (int)row["penya"];
            FlyingLevel = (int)row["flyingLevel"];
            FlyingExp = (int)row["flyingExp"];
            HP = (int)row["hp"];
            MP = (int)row["mp"];
            Size = (int)row["size"];
            PvPPoints = (int)row["pvpPoints"];
            PKPoints = (int)row["pkPoints"];
            GuildId = (int)row["guildId"];
            Bag1TimeLeft = (int)row["bag1TimeLeft"];
            Bag2TimeLeft = (int)row["bag2TimeLeft"];
            MsgState = (int)row["msgState"];
            MotionFlags = (int)row["motionFlags"];
            MovementFlags = (int)row["movementFlags"];
            PlayerFlags = (int)row["playerFlags"];
        }
    }
}
