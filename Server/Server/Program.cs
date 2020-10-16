using System;

namespace ServerHP
{
    class Program
    {
        private static void Main(string[] args)
        {
            Game game = new Game();
            IServer server = new HttpServer();
            server.ProcessRequest = game.ProcessRequest;

            server.Start();

            do
            {
                Console.WriteLine("\nTo shutdown server press 'q'...");
            } while (Console.ReadKey().KeyChar != 'q');
        }
    }
}