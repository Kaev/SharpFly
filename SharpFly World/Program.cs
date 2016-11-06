using SharpFly_World.Server;
using System;
using System.Runtime.InteropServices;

namespace SharpFly_World
{
    class Program
    {
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);
        private static ConsoleEventDelegate m_Handler;

        public static WorldServer WorldServer;

        static void Main(string[] args)
        {
            m_Handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(m_Handler, true);

            WorldServer = new WorldServer();
            Console.ReadLine();
            WorldServer.Dispose();
        }

        // Clean up all active socket before closing the server
        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
                WorldServer.Dispose();
            return false;
        }
    }
}
