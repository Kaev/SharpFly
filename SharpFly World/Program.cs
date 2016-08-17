using System;
using SharpFly_World.Server;

namespace SharpFly_World
{
    class Program
    {
        public static WorldServer WorldServer;

        static void Main(string[] args)
        {
            WorldServer = new WorldServer(28000);
            Console.ReadLine();
        }
    }
}
