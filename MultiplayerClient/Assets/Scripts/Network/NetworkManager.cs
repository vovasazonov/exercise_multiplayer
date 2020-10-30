using System;
using System.Threading.Tasks;
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
        private bool _isNetworkManagerRun;
        private readonly Task _sendPacketLoopTask;
        private readonly ICustomPacket _everyTickToServerPacket;
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
            _everyTickToServerPacket = new CustomPacket(_serializer);
            _mainGameEventHandler = new MainGameEventHandler(_everyTickToServerPacket, _modelManagerClient.ModelManager);
            _mainGameEventHandler.Activate();
            
            _sendPacketLoopTask = new Task(StartSendPacketLoop);
            _sendPacketLoopTask.Start();

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

        private void StartSendPacketLoop()
        {
            _isNetworkManagerRun = true;

            while (_isNetworkManagerRun)
            {
                Task.Delay(_millisecondsBetweenSend).Wait();

                if (_everyTickToServerPacket.Data.Length > 0)
                {
                    _client.SendPacket(_everyTickToServerPacket.Data);
                    _everyTickToServerPacket.Clear();
                }
            }
        }

        private void OnPacketReceived(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
        {
            var packet = new CustomPacket(_serializer);
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
            _isNetworkManagerRun = false;
            _sendPacketLoopTask.Wait();
            _client?.Dispose();
        }
    }
}