using System;
using System.Collections.Generic;
using Models;
using Network.GameEventHandlers;
using Network.PacketHandlers;
using Serialization;

namespace Network
{
    public class NetworkManager : IDisposable
    {
        private readonly IServer _server;
        private readonly ISerializer _serializer;
        private readonly IModelManager _modelManager;
        private readonly IDictionary<uint, IClientProxy> _clientProxyDic = new Dictionary<uint, IClientProxy>();
        private readonly ServerGameProcessor _gameProcessor;
        private readonly IGameEventHandler _mainGameEventHandler;

        public int MillisecondsTickServer
        {
            set => _gameProcessor.MillisecondsTick = value;
        }

        public NetworkManager(IServer server, ISerializer serializer, IModelManager modelManager)
        {
            _server = server;
            _serializer = serializer;
            _modelManager = modelManager;
            _gameProcessor = new ServerGameProcessor(_clientProxyDic, _modelManager);
            _mainGameEventHandler = new MainGameEventHandler(_clientProxyDic, _modelManager);

            _mainGameEventHandler.Activate();
            AddServerListener();
            AddGameProcessorListener();
        }

        private void AddGameProcessorListener()
        {
            _gameProcessor.Processed += OnGameProcessorProcessed;
        }

        private void RemoveGameProcessorListener()
        {
            _gameProcessor.Processed -= OnGameProcessorProcessed;
        }

        private void AddServerListener()
        {
            _server.PacketReceived += OnPacketReceived;
            _server.ClientConnect += OnClientConnect;
            _server.ClientDisconnect += OnClientDisconnect;
        }

        private void RemoveServerListener()
        {
            _server.PacketReceived -= OnPacketReceived;
            _server.ClientConnect -= OnClientConnect;
            _server.ClientDisconnect -= OnClientDisconnect;
        }

        private void OnGameProcessorProcessed(object sender, EventArgs eventArgs)
        {
            SendPacketsToClients();
        }

        private void OnClientDisconnect(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
        {
            IPacketHandler packetHandler = new DisconnectPacketHandler(packetReceivedEventArgs.ClientId, _clientProxyDic);
            packetHandler.HandlePacket();
        }

        private void OnClientConnect(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
        {
            IPacketHandler packetHandler = new ConnectPacketHandler(packetReceivedEventArgs.ClientId, _clientProxyDic, _serializer, _modelManager);
            packetHandler.HandlePacket();
        }

        private void OnPacketReceived(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
        {
            IPacketHandler packetHandler = new CommandPacketHandler(packetReceivedEventArgs.ClientId, packetReceivedEventArgs.Packet, _clientProxyDic);
            packetHandler.HandlePacket();
        }

        private void SendPacketsToClients()
        {
            foreach (var clientProxy in _clientProxyDic.Values)
            {
                if (clientProxy.NotSentToClientPacket.Data.Length > 0)
                {
                    _server.SendPacket(clientProxy.Id, clientProxy.NotSentToClientPacket.Data);
                    clientProxy.NotSentToClientPacket.Clear();
                }
            }
        }

        public void Dispose()
        {
            _mainGameEventHandler.Deactivate();
            RemoveServerListener();
            RemoveGameProcessorListener();

            _server?.Dispose();
            _gameProcessor?.Dispose();
        }
    }
}