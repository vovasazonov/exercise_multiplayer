using System;
using Network.GameEventHandlers;
using Network.PacketHandlers;
using Serialization;

namespace Network
{
    public class NetworkManager : IDisposable
    {
        private readonly IClient _client;
        private readonly ISerializer _serializer;
        private readonly IModelManagerClient _modelManagerClient;
        private readonly IMutablePacket _everyTickToServerPacket;
        private int _millisecondsBetweenSend;
        private readonly IGameEventHandler _mainGameEventHandler;

        public int MillisecondsBetweenSendPacket
        {
            set => _millisecondsBetweenSend = value;
        }

        public NetworkManager(IClient client, ISerializer serializer, IModelManagerClient modelManagerClient)
        {
            _client = client;
            _serializer = serializer;
            _modelManagerClient = modelManagerClient;
            _everyTickToServerPacket = new MutablePacket(_serializer);
            _mainGameEventHandler = new MainGameEventHandler(_everyTickToServerPacket, _modelManagerClient.ModelManager);
            _mainGameEventHandler.Activate();
            
            AddClientListeners();
        }

        private void AddClientListeners()
        {
            _client.ClientConnected += OnClientConnected;
            _client.ClientDisconnected += OnClientDisconnected;
            _client.PacketReceived += OnPacketReceived;
        }

        private void RemoveClientListeners()
        {
            _client.ClientConnected -= OnClientConnected;
            _client.ClientDisconnected -= OnClientDisconnected;
            _client.PacketReceived -= OnPacketReceived;
        }

        public void Update()
        {
            SendPacket();
        }
        
        private void SendPacket()
        {
            if (_everyTickToServerPacket.Data.Length > 0)
            {
                _client.SendPacket(_everyTickToServerPacket.Data);
                _everyTickToServerPacket.Clear();
            }
        }

        private void OnPacketReceived(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
        {
            var packet = new MutablePacket(_serializer);
            packet.Fill(packetReceivedEventArgs.Packet);
            IPacketHandler packetHandler = new CommandPacketHandler(packet, _modelManagerClient);
            packetHandler.HandlePacket();
        }

        private void OnClientDisconnected(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
        {
            IPacketHandler packetHandler = new ClientDisconnectedPacketHandler();
            packetHandler.HandlePacket();
        }

        private void OnClientConnected(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
        {
            IPacketHandler packetHandler = new ClientConnectedPacketHandler();
            packetHandler.HandlePacket();
        }

        public void Dispose()
        {
            _mainGameEventHandler.Deactivate();
            RemoveClientListeners();
        }
    }
}