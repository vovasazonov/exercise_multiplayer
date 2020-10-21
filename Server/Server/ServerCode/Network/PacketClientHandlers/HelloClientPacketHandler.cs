using System;
using System.Collections.Generic;
using Game;
using Network;
using Serialization;

namespace Server.Network.PacketClientHandlers
{
    public struct HelloClientPacketHandler : IClientPacketHandler
    {
        private readonly IDictionary<int, IClientProxy> _clients;
        private readonly ISerializer _serializer;
        private int _lastSetId;
        private readonly Queue<byte> _responsePacket;
        private readonly IModelManager _modelManager;

        public HelloClientPacketHandler(IDictionary<int, IClientProxy> clients, ISerializer serializer, Queue<byte> responsePacket, IModelManager modelManager)
        {
            _clients = clients;
            _serializer = serializer;
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
            foreach (var idCharacterPair in _modelManager.CharacterModelDic)
            {
                clientProxy.NotSentPacketCommands.Enqueue(_serializer.Serialize(GameCommandType.CharacterHpChanged));
                clientProxy.NotSentPacketCommands.Enqueue(_serializer.Serialize(idCharacterPair.Key));
                clientProxy.NotSentPacketCommands.Enqueue(_serializer.Serialize(idCharacterPair.Value.HealthPoint.Points));
            }
        }
    }
}