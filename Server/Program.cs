using System;
using Server.Network;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IServer server = new UdpServer();
            
            Console.WriteLine("Press 'q' to stop server");
            while (Console.ReadKey().Key != ConsoleKey.Q)
            {
            }
        }
    }
}