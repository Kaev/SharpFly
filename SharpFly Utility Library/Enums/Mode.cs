namespace SharpFly_Utility_Library.Enums
{
    public enum Mode : uint
    {
        NONE = 0x0,
        MATCHLESS = 0x1, // Invincibility
        TRANSPARENT = 0x2, // Invisible
        ONEKILL = 0x4, // ?
        DONTMOVE = 0x8, // Can't move
        SAYTALK = 0x10, // Can't whisper
        MATCHLESS2 = 0x20, // Invincibility 2 (Gets damage but can't die)
        NO_ATTACK = 0x40, // Can't attack
        ITEM = 0x80, // Can't throw away items
        COMMUNITY = 0x100, // No guild, party, friend, individual transaction, banking allowed
        TALK = 0x200, // Can not speak japanese (Why is that a flag?)
        SHOUTTALK = 0x400, // Unable to shout
        RECOVERCHAOS = 0x800, // Chaos overcoming mode?
        FREEPK = 0x1000, // Can PK without pressing CTRL
        PVPCONFIRM = 0x2000, // PvP rejection status
        QUERYSETPLAYERNAME = 0x4000, // Character name changeable
        MAILBOX = 0x8000, // Unread letter
        EVENT_OLDBOY = 0x10000, // ?
        EQUIP_DENIAL = 0x20000, // ?
        EXPUP_STOP = 0x40000, // Can't get experience
        GCWAR_NOT_CLICK = 0x80000, // Can not click on themselves
        GCWAR_RENDER_SKIP = 0x100000, // Skip rendering
        QUIZ_RENDER_SKIP = 0x200000, // Skips rendering in quiz event area
        OPTION_DONT_RENDER_MASK = 0x1000000, // Whether the mask is rendered
        FRESH = 0x10000000, // First person to connect (Why?)
        NOTFRESH = 0x20000000, // Needs to change face
        NOTFRESH2 = 0x40000000, // Needs to change face 2
        OUTOF_PARTYQUESTREGION = 0x80000000, // Party outside of quest
        DONTTALK = 0x610, // TALK, SAYTALK, SHOUTTALK combined
        OBSERVE = 0xC0, // COMMUNITY, ITEM, ATTACK, SHOUTTALK, SAYTALK
        ALL = 0x5D0, // All modes
    }
}
