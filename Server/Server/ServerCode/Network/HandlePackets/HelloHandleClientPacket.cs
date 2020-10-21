using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using Network;
using Serialization;

namespace Server.Network.HandlePackets
{
    public struct HelloHandleClientPacket : IHandleClientPacket
    {
        private readonly Dictionary<int, ClientProxy> _clients;
        private readonly ISerializer _serializer;
        private int _lastSetId;
        private readonly Queue<byte> _packetCame;
        private readonly Queue<byte> _responsePacket;
        private readonly ModelManager _modelManager;

        public HelloHandleClientPacket(Dictionary<int, ClientProxy> clients, ISerializer serializer, Queue<byte> packetCame, Queue<byte> responsePacket, ModelManager modelManager)
        {
            _clients = clients;
            _serializer = serializer;
            _packetCame = packetCame;
            _lastSetId = Int32.MaxValue + _clients.Count;
            _responsePacket = responsePacket;
            _modelManager = modelManager;
        }

        public void HandlePacket()
        {
            bool newClientSet = false;
            int fuse = _lastSetId;

            while (!newClientSet && ++_lastSetId != fuse)
            {
                if (!_clients.Keys.Contains(_lastSetId))
                {
                    var clientProxy = new ClientProxy(_lastSetId);
                    _clients[_lastSetId] = clientProxy;
                    MoveServerDataToClient(clientProxy);
                    PrepareResponsePacket(clientProxy);
                    newClientSet = true;
                }
            }
        }

        private void PrepareResponsePacket(ClientProxy clientProxy)
        {
            _responsePacket.Enqueue(_serializer.Serialize(NetworkPacketType.Welcome));
            _responsePacket.Enqueue(_serializer.Serialize(clientProxy.IdClient));
        }

        private void MoveServerDataToClient(ClientProxy clientProxy)
        {
            foreach (var keyValuePair in _modelManager.CharacterModelDic)
            {
                clientProxy.NotSentPacketCommands.Enqueue(_serializer.Serialize(GameCommandType.CharacterHpChanged));
                clientProxy.NotSentPacketCommands.Enqueue(_serializer.Serialize(keyValuePair.Key));
                clientProxy.NotSentPacketCommands.Enqueue(_serializer.Serialize(keyValuePair.Value.HealthPoint.Points));
            }
        }
    }
}