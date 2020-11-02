using System;
using System.Collections.Generic;
using Models;
using Models.Characters;
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
            
            var random = new Random();
            var newCharacterExemplarId = random.NextExclude(_modelManager.CharacterModelDic.Keys);
            CreatePlayerCharacter(newCharacterExemplarId);
            playerModel.ControllableCharacterExemplarId = newCharacterExemplarId;
            _modelManager.PlayerModelDic.Add((int) _clientId, playerModel);
        }
        
        private void CreatePlayerCharacter(int newCharacterExemplarId)
        {
            var characterData = new CharacterData
            {
                Id = "player_character",
                HealthPointData = new HealthPointData {MaxPoints = 100, Points = 100}
            };
            var characterModel = new CharacterModel(characterData);
            
            _modelManager.CharacterModelDic.Add(newCharacterExemplarId, characterModel);
        }
    }
}