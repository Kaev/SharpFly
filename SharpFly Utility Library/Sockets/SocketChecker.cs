using System.Net.Sockets;

namespace SharpFly_Utility_Library.Sockets
{
    public static class SocketChecker
    {
        public static bool IsSocketConnected(Socket s)
        {
            return !((s.Poll(1000, SelectMode.SelectRead) && (s.Available == 0)) || !s.Connected);
        }
    }
}
