using System;
using System.Collections.Generic;
using Game;
using Serialization;
using Serialization.BinaryFormatterSerialization;
using Server.Network.PacketClientHandlers;

namespace Server.Network
{
    public class NetworkManagerServer : IDisposable
    {
        private readonly IServer _server;
        private readonly IModelManager _modelManager;
        private readonly IDictionary<int, IClientProxy> _clientProxyDic = new Dictionary<int, IClientProxy>();
        private readonly ISerializer _serializer = new BinaryFormatterSerializer();
        private readonly int _millisecondsTick = 500;
        private readonly IClientCommandsProcessor _clientCommandsProcessor;

        public NetworkManagerServer(IServer server, IModelManager modelManager)
        {
            _server = server;
            _modelManager = modelManager;
            _clientCommandsProcessor = new ClientCommandsProcessor(modelManager, _clientProxyDic, _serializer, _millisecondsTick);

            AddServerListener();
            _clientCommandsProcessor.Start();
        }

        private void AddServerListener()
        {
            _server.ClientPacketCame += OnClientPacketCame;
        }

        private void RemoveServerListener()
        {
            _server.ClientPacketCame -= OnClientPacketCame;
        }

        private void OnClientPacketCame(Queue<byte> packetCame, Queue<byte> packetResponse)
        {
            IClientPacketHandler clientPacketHandler = new MainClientPacketHandler(packetCame,packetResponse,_clientProxyDic,_modelManager,_serializer);
            clientPacketHandler.HandlePacket();
        }

        public void Dispose()
        {
            _clientCommandsProcessor.Stop();
            RemoveServerListener();
        }
    }
}