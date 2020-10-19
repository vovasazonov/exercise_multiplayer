using System;
using Server.Network;

namespace Server
{
    class Program
    {
        private static void Main(string[] args)
        {
            IServer server = new HttpServer();
            NetworkManagerServer networkManagerServer = new NetworkManagerServer(server);

            server.Start();
            
            Console.WriteLine("Press enter to shut down server...");
            Console.ReadLine();
            server.Stop();
        }
    }
}