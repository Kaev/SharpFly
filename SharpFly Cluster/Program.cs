using System;
using System.Runtime.InteropServices;
using SharpFly_Cluster.Server;

namespace SharpFly_Cluster
{
    class Program
    {
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);
        private static ConsoleEventDelegate m_Handler;

        public static ClusterServer ClusterServer;

        static void Main(string[] args)
        {
            m_Handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(m_Handler, true);

            ClusterServer = new ClusterServer();
            Console.ReadLine();
            ClusterServer.Dispose();
        }

        // Clean up all active socket before closing the server
        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
                ClusterServer.Dispose();
            return false;
        }
    }
}
