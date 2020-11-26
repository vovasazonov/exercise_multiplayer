using System;
using System.Collections.Generic;
using Network.GameEventHandlers;
using Network.PacketHandlers;
using Replications;
using Serialization;

namespace Network
{
    public class NetworkManager : IDisposable
    {
        private readonly IClient _client;
        private readonly IModelManagerClient _modelManagerClient;
        private readonly IReplication _worldDataReplication;
        private readonly IDataMutablePacket _everyTickToServerPacket;
        private readonly IGameEventHandler _mainGameEventHandler;
        private readonly IDataMutablePacket _receivedPackets;
        private int _millisecondsBetweenSend;
        private DateTime _lastTimeSend;

        public int MillisecondsBetweenSendPacket
        {
            set => _millisecondsBetweenSend = value;
        }

        public NetworkManager(IClient client, ISerializer serializer, IModelManagerClient modelManagerClient, IReplication worldDataReplication)
        {
            _client = client;
            _receivedPackets = new DataMutablePacket(serializer);
            _modelManagerClient = modelManagerClient;
            _worldDataReplication = worldDataReplication;
            _everyTickToServerPacket = new DataMutablePacket(serializer);
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
            ProcessReceivedPackets();
        }

        private void ProcessReceivedPackets()
        {
            var dataTypes = new List<DataType>(_receivedPackets.MutablePacketDic.Keys);
            foreach (var dataType in dataTypes)
            {
                IPacketHandler packetHandler;
                var mutablePacket = _receivedPackets.MutablePacketDic[dataType];
                switch (dataType)
                {
                    case DataType.Command:
                        packetHandler = new CommandDataPacketHandler(mutablePacket, _modelManagerClient);
                        break;
                    case DataType.State:
                        packetHandler = new StateDataPacketHandler(mutablePacket, _worldDataReplication);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                packetHandler.HandlePacket();
            }
        }

        private void SendPacket()
        {
            if (CheckPermissionSend())
            {
                _client.SendPacket(_everyTickToServerPacket.PullCombinedData());
            }
        }

        private bool CheckPermissionSend()
        {
            var differenceTimeBetweenSend = DateTime.Now - _lastTimeSend;
            var havePermission = differenceTimeBetweenSend.TotalMilliseconds > _millisecondsBetweenSend;

            if (havePermission)
            {
                _lastTimeSend = DateTime.Now;
            }

            return havePermission;
        }

        private void OnPacketReceived(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
        {
            _receivedPackets.FillCombinedData(packetReceivedEventArgs.Packet);
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