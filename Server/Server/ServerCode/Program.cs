using System;
using Game;
using Server.Network;

namespace Server
{
    class Program
    {
        private static void Main(string[] args)
        {
            IServer server = new HttpServer();
            IModelManager modelManager = new ModelManager();
            NetworkManagerServer networkManagerServer = new NetworkManagerServer(server, modelManager);

            server.Start();

            Console.WriteLine("Press enter to shut down server...");
            Console.ReadLine();
            server.Stop();
        }
    }
}