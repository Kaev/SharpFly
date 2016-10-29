using System;
using System.Runtime.InteropServices;
using SharpFly_Login.Server;
using SharpFly_Login.Server.Interserver;
using System.Threading.Tasks;
using System.Threading;

namespace SharpFly_Login
{
    class Program
    {
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);
        private static ConsoleEventDelegate m_Handler;

        public static LoginServer LoginServer;

        static void Main(string[] args)
        {
            m_Handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(m_Handler, true);

            LoginServer = new LoginServer(23000);
            Console.ReadLine();
            LoginServer.Dispose();
        }

        // Clean up all active socket before closing the server
        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
            {
                LoginServer.ClientManager.Dispose();
                LoginServer.ClusterManager.Dispose();
            }
            return false;
        }
    }
}
