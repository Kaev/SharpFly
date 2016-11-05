using SharpFly_Utility_Library.Math;
using System;
using System.Data;

namespace SharpFly_Utility_Library.Database.ClusterDatabase.Tables
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public uint ClusterId { get; set; }
        public int ClassId { get; set; }
        public uint Skinset { get; set; }
        public uint HairStyle { get; set; }
        public uint HairColor { get; set; }
        public uint HeadMesh { get; set; }
        public uint Face { get; set; }
        public byte Gender { get; set; }
        public uint Strength { get; set; }
        public uint Stamina { get; set; }
        public uint Dexterity { get; set; }
        public uint Intelligence { get; set; }
        public uint SkillPoints { get; set; }
        public uint StatPoints { get; set; }
        public int Level { get; set; }
        public uint Experience { get; set; }
        public uint Map { get; set; }
        public Vector4<float> Position { get; set; }
        public uint Penya { get; set; }
        public uint FlyingLevel { get; set; }
        public uint FlyingExp { get; set; }
        public uint HP { get; set; }
        public uint MP { get; set; }
        public uint Size { get; set; }
        public int PvPPoints { get; set; }
        public int PKPoints { get; set; }
        public uint GuildId { get; set; }
        public uint Bag1TimeLeft { get; set; }
        public uint Bag2TimeLeft { get; set; }
        public uint MsgState { get; set; }
        public uint MotionFlags { get; set; }
        public uint MovementFlags { get; set; }
        public uint PlayerFlags { get; set; }

        public Character() { }

        public Character(DataRow row)
        {
            CharacterId = (int)row["characterId"];
            Name = (string)row["name"];
            ClusterId = Convert.ToUInt32(row["clusterId"]);
            ClassId = Convert.ToInt32(row["classId"]);
            Skinset = Convert.ToByte(row["skinset"]);
            HairStyle = Convert.ToByte(row["hairStyle"]);
            HairColor = Convert.ToUInt32(row["hairColor"]);
            HeadMesh = Convert.ToUInt32(row["headMesh"]);
            Face = Convert.ToByte(row["face"]);
            Gender = Convert.ToByte(row["gender"]);
            Strength = Convert.ToUInt32(row["strength"]);
            Stamina = Convert.ToUInt32(row["stamina"]);
            Dexterity = Convert.ToUInt32(row["dexterity"]);
            Intelligence = Convert.ToUInt32(row["intelligence"]);
            SkillPoints = Convert.ToUInt32(row["skillpoints"]);
            StatPoints = Convert.ToUInt32(row["statpoints"]);
            Level = Convert.ToInt32(row["level"]);
            Experience = Convert.ToUInt32(row["exp"]);
            Map = Convert.ToUInt32(row["map"]);
            Position = new Vector4<float>(Convert.ToSingle(row["x"]), Convert.ToSingle(row["y"]), Convert.ToSingle(row["z"]), Convert.ToSingle(row["orientation"]));
            Penya = Convert.ToUInt32(row["penya"]);
            FlyingLevel = Convert.ToUInt32(row["flyingLevel"]);
            FlyingExp = Convert.ToUInt32(row["flyingExp"]);
            HP = Convert.ToUInt32(row["hp"]);
            MP = Convert.ToUInt32(row["mp"]);
            Size = Convert.ToUInt32(row["size"]);
            PvPPoints = (int)row["pvpPoints"];
            PKPoints = (int)row["pkPoints"];
            GuildId = Convert.ToUInt32(row["guildId"]);
            Bag1TimeLeft = Convert.ToUInt32(row["bag1TimeLeft"]);
            Bag2TimeLeft = Convert.ToUInt32(row["bag2TimeLeft"]);
            MsgState = Convert.ToUInt32(row["msgState"]);
            MotionFlags = Convert.ToUInt32(row["motionFlags"]);
            MovementFlags = Convert.ToUInt32(row["movementFlags"]);
            PlayerFlags = Convert.ToUInt32(row["playerFlags"]);
        }
    }
}
