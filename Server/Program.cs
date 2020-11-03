using System;
using Models;
using Network;
using Serialization;
using Serialization.JsonNetSerialization;

class Program
{
    static void Main(string[] args)
    {
        IModelManager modelManager = new ModelManager();
        ISerializer serializer = new JsonNetSerializer();
        var udpServerInfo = new LocalUdpServerInfoCreator().Create();
        using IServer server = new UdpServer(udpServerInfo);
        NetworkManager networkManager = new NetworkManager(server, serializer, modelManager) {MillisecondsTick = 100};

        networkManager.Start();
        Console.WriteLine("Press 'q' to stop server");
        while (Console.ReadKey().Key != ConsoleKey.Q)
        {
        }
        networkManager.Stop();
    }
}