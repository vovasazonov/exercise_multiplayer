using System;
using System.Collections.Generic;
using Models;
using Network.CommandHandlers;
using Network.GameEventHandlers;
using Network.PacketHandlers;
using Serialization;

namespace Network
{
    public class NetworkManager
    {
        private readonly IServer _server;
        private readonly ISerializer _serializer;
        private readonly IModelManager _modelManager;
        private readonly IDictionary<uint, IClientProxy> _clientProxyDic = new Dictionary<uint, IClientProxy>();
        private readonly IGameEventHandler _mainGameEventHandler;
        private readonly TickSystem _tickSystem;
        private readonly Queue<ClientMessage> _messageQueue = new Queue<ClientMessage>();

        public NetworkManager(IServer server, ISerializer serializer, IModelManager modelManager, int millisecondsTick)
        {
            _server = server;
            _serializer = serializer;
            _modelManager = modelManager;
            _mainGameEventHandler = new MainGameEventHandler(_clientProxyDic, _modelManager);
            _tickSystem = new TickSystem {MillisecondsTick = millisecondsTick};
        }

        public void Start()
        {
            _mainGameEventHandler.Activate();
            AddServerListener();
            AddTickSystemListener();
            _tickSystem.Start();
        }

        public void Stop()
        {
            _mainGameEventHandler.Deactivate();
            RemoveServerListener();
            RemoveTickSystemListener();
            _tickSystem.Stop();
        }

        private void HandleUnprocessedClients()
        {
            foreach (var clientProxy in _clientProxyDic.Values)
            {
                HandleUnprocessedCommands(clientProxy);
            }
        }

        private void HandleUnprocessedCommands(IClientProxy clientProxy)
        {
            while (clientProxy.UnprocessedReceivedPacket.Data.Length > 0)
            {
                ICommandHandler commandHandler = new MainCommandHandler(clientProxy.UnprocessedReceivedPacket, _modelManager);
                commandHandler.HandleCommand();
            }
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

        private void AddTickSystemListener()
        {
            _tickSystem.TickStart += OnTickStart;
        }

        private void RemoveTickSystemListener()
        {
            _tickSystem.TickStart -= OnTickStart;
        }

        private void OnTickStart(object sender, EventArgs e)
        {
            FreeMessageQueue();
            HandleUnprocessedClients();
            SendPacketsToClients();
        }

        private void FreeMessageQueue()
        {
            while (_messageQueue.Count > 0)
            {
                var message = _messageQueue.Dequeue();
                IPacketHandler packetHandler = null;

                switch (message.MessageType)
                {
                    case MessageType.Connect:
                        packetHandler = new ConnectPacketHandler(message.ClientId, _clientProxyDic, _serializer, _modelManager);
                        break;
                    case MessageType.Disconnect:
                        packetHandler = new DisconnectPacketHandler(message.ClientId, _clientProxyDic, _modelManager);
                        break;
                    case MessageType.Command:
                        packetHandler = new CommandPacketHandler(message.ClientId, message.Packet, _clientProxyDic);
                        break;
                }

                packetHandler?.HandlePacket();
            }
        }

        private void OnClientDisconnect(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
        {
            var packet = new ClientMessage(packetReceivedEventArgs.ClientId,MessageType.Disconnect, packetReceivedEventArgs.Packet);
            _messageQueue.Enqueue(packet);
        }

        private void OnClientConnect(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
        {
            var packet = new ClientMessage(packetReceivedEventArgs.ClientId, MessageType.Connect, packetReceivedEventArgs.Packet);
            _messageQueue.Enqueue(packet);
        }

        private void OnPacketReceived(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
        {
            var packet = new ClientMessage(packetReceivedEventArgs.ClientId, MessageType.Command, packetReceivedEventArgs.Packet);
            _messageQueue.Enqueue(packet);
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
    }
}