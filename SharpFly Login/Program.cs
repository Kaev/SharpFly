using System;
using SharpFly_Login.Server;

namespace SharpFly_Login
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginServer server = new LoginServer(23000);
            Console.ReadLine();
        }
    }
}
