using System;
using System.Collections.Generic;
using Descriptions;
using Game;
using Server.Descriptions;
using Server.Network;

namespace Server
{
    class Program
    {
        private static void Main(string[] args)
        {
            IDescriptionManager descriptionManager = new DescriptionManager();
            IDictionary<int, IClientProxy> clientProxyDic = new Dictionary<int, IClientProxy>();
            IServer server = new HttpServer(clientProxyDic);
            IModelManager modelManager = new ModelManager();
            using NetworkManagerServer networkManagerServer = new NetworkManagerServer(server, modelManager, clientProxyDic);
            
            server.Start();

            Console.WriteLine("Press enter to shut down server...");
            Console.ReadLine();
            server.Stop();
        }
    }
}