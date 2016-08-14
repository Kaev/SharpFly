using System;
using SharpFly_Login.Server;

namespace SharpFly_Login
{
    class Program
    {
        public static LoginServer LoginServer;

        static void Main(string[] args)
        {
            LoginServer = new LoginServer(23000);
            Console.ReadLine();
            LoginServer.Close();
        }
    }
}
