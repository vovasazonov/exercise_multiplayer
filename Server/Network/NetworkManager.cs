using System;
using System.Collections.Generic;
using Models;
using Network.DataHandlers;
using Network.PacketHandlers;
using Serialization;

namespace Network
{
    public class NetworkManager
    {
        private readonly IServer _server;
        private readonly ISerializer _serializer;
        private readonly WorldData _worldData;
        private readonly GameManagerServer _gameManagerServer;
        private readonly ITrackableDictionary<uint, IClientProxy> _clientProxyDic = new TrackableDictionary<uint, IClientProxy>();
        private readonly ITickSystem _tickSystem;
        private readonly Queue<IClientMessage> _messageQueue = new Queue<IClientMessage>();

        public NetworkManager(IServer server, ISerializer serializer, IModelManager modelManager, WorldData worldData,int millisecondsTick)
        {
            _server = server;
            _serializer = serializer;
            _worldData = worldData;

            _gameManagerServer = new GameManagerServer(_clientProxyDic, modelManager, worldData);
            _tickSystem = new TickSystem {MillisecondsTick = millisecondsTick};
        }

        public void Start()
        {
            AddServerListener();
            AddTickSystemListener();
            _tickSystem.Start();
        }

        public void Stop()
        {
            RemoveServerListener();
            RemoveTickSystemListener();
            _tickSystem.Stop();
        }

        private void HandleUnprocessedClients()
        {
            foreach (var clientProxy in _clientProxyDic.Values)
            {
                HandleUnprocessedClientData(clientProxy);
            }
        }

        private void HandleUnprocessedClientData(IClientProxy clientProxy)
        {
            foreach (var dataType in clientProxy.UnprocessedReceivedPacket.MutablePacketDic.Keys)
            {
                var mutablePacket = clientProxy.UnprocessedReceivedPacket.MutablePacketDic[dataType];
                IDataHandler dataHandler = new DataHandler(mutablePacket, _gameManagerServer);

                dataHandler.HandleData();
                mutablePacket.Clear();
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
            CreateSnapshotToClients();
            SendPacketsToClients();
        }

        private void CreateSnapshotToClients()
        {
            byte[] whole = null;
            byte[] diff = null;

            foreach (var clientProxy in _clientProxyDic.Values)
            {
                if (clientProxy.IsFirstWhole)
                {
                    whole ??= GetSnapshot(ReplicationType.Whole);
                    clientProxy.IsFirstWhole = false;
                    clientProxy.NotSentToClientPacket.FillCombinedData(whole);
                }
                else
                {
                    diff ??= GetSnapshot(ReplicationType.Diff);
                    clientProxy.NotSentToClientPacket.FillCombinedData(diff);
                }
            }
        }

        private byte[] GetSnapshot(ReplicationType replicationType)
        {
            var mutablePacket = new MutablePacket(_serializer);
            mutablePacket.Fill(DataType.State);
            mutablePacket.Fill(_worldData.Write(replicationType));

            return mutablePacket.Data;
        }

        private void FreeMessageQueue()
        {
            while (_messageQueue.Count > 0)
            {
                var message = _messageQueue.Dequeue();

                IPacketHandler packetHandler = new PacketHandler(message, _serializer, _clientProxyDic);
                packetHandler.HandlePacket();
            }
        }

        private void SendPacketsToClients()
        {
            foreach (var clientProxy in _clientProxyDic.Values)
            {
                _server.SendPacket(clientProxy.Id, clientProxy.NotSentToClientPacket.PullCombinedData(), true);
            }
        }

        private void OnClientDisconnect(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
        {
            var clientMessage = new ClientMessage(packetReceivedEventArgs.ClientId, MessageType.Disconnect);
            _messageQueue.Enqueue(clientMessage);
        }

        private void OnClientConnect(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
        {
            var clientMessage = new ClientMessage(packetReceivedEventArgs.ClientId, MessageType.Connect);
            _messageQueue.Enqueue(clientMessage);
        }

        private void OnPacketReceived(object sender, PacketReceivedEventArgs packetReceivedEventArgs)
        {
            var clientMessage = new ClientMessage(packetReceivedEventArgs.ClientId, MessageType.Data, packetReceivedEventArgs.Packet);
            _messageQueue.Enqueue(clientMessage);
        }
    }
}