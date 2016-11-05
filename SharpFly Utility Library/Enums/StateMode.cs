namespace SharpFly_Utility_Library.Enums
{
    public enum StateMode : uint
    {
        PK_MODE = 0x1,
        PVP_MODE = 0x2,
        BASEMOTION_MODE = 0x4,
        BASEMOTION = 0xc // BASEMOTION_MODE + BASEMOTION_END_MODE, what is BASEMOTION_END_MODE?
    }
}
