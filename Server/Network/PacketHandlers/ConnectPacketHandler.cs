using System.Collections.Generic;
using Models;
using Serialization;

namespace Network.PacketHandlers
{
    public readonly struct ConnectPacketHandler : IPacketHandler
    {
        private readonly uint _clientId;
        private readonly IDictionary<uint, IClientProxy> _clientProxyDic;
        private readonly ISerializer _serializer;
        private readonly IModelManager _modelManager;

        public ConnectPacketHandler(in uint clientId, IDictionary<uint, IClientProxy> clientProxyDic, ISerializer serializer, IModelManager modelManager)
        {
            _clientId = clientId;
            _clientProxyDic = clientProxyDic;
            _serializer = serializer;
            _modelManager = modelManager;
        }

        public void HandlePacket()
        {
            var newClientProxy = new ClientProxy(_clientId, _serializer);
            _clientProxyDic.Add(_clientId, newClientProxy);
            var playerModel = new PlayerModel(_modelManager.CharacterModelDic);
            _modelManager.PlayerModelDic.Add((int) _clientId, playerModel);
            TransferWorldToNewPlayer(newClientProxy);
            _clientProxyDic[_clientId].NotSentToClientPacket.Fill(GameCommandType.SetControllablePlayer);
            _clientProxyDic[_clientId].NotSentToClientPacket.Fill((int)_clientId);
        }
        
        private void TransferWorldToNewPlayer(IClientProxy newClientProxy)
        {
            var packet = newClientProxy.NotSentToClientPacket;

            foreach (var characterExemplarId in _modelManager.CharacterModelDic.Keys)
            {
                packet.Fill(GameCommandType.CharacterAdd);
                packet.Fill(characterExemplarId);
                packet.Fill(_modelManager.CharacterModelDic[characterExemplarId].Id);
                packet.Fill(_modelManager.CharacterModelDic[characterExemplarId].HealthPoint.MaxPoints);
                packet.Fill(_modelManager.CharacterModelDic[characterExemplarId].HealthPoint.Points);
            }

            foreach (var playerExemplarId in _modelManager.PlayerModelDic.Keys)
            {
                packet.Fill(GameCommandType.PlayerConnected);
                packet.Fill(playerExemplarId);
                packet.Fill(_modelManager.PlayerModelDic[playerExemplarId].ControllableCharacterExemplarId);
            }
        }
    }
}