namespace SharpFly_Packet_Library.Packets.LoginServer.Outgoing
{
    public static class OpCodes
    {
        public const uint SESSION_KEY = 0x00;
        public const uint PING = 0x0B;
        public const uint SERVER_LIST = 0xFD;
        public const uint LOGIN_MESSAGE = 0xFE;
    }

    public static class LoginError
    {
        public const uint ERROR_SERVICE_DOWN = 0x6D;
        public const uint ERROR_ACCOUNT_CONNECTED = 0x67;
        public const uint ERROR_ACCOUNT_BANNED = 0x77;
        public const uint ERROR_INVALID_PASSWORD = 0x78;
        public const uint ERROR_INVALID_USERNAME = 0x79;
        public const uint ERROR_VERIFICATION_REQUIRED = 0x7A;
        public const uint ERROR_ACCOUNT_MAINTENANCE = 0x85;
        public const uint ERROR_TEMPBAN_WRONGPW_15SECONDS = 0x86;
        public const uint ERROR_TEMPBAN_WRONGPW_15MINUTES = 0x87;
        public const uint ERROR_SERVER_ERROR = 0x88;
        public const uint ERROR_RESOURCE_FALSIFIED = 0x8A;
    }
}
