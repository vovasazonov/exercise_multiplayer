using System;
using System.Collections.Generic;
using Models;
using Models.Characters;

namespace Network.GameEventHandlers
{
    public class PlayerAddingEventHandler : IGameEventHandler
    {
        private readonly IDictionary<uint, IClientProxy> _clientProxyDic;
        private readonly IModelManager _modelManager;

        public PlayerAddingEventHandler(IDictionary<uint, IClientProxy> clientProxyDic, IModelManager modelManager)
        {
            _clientProxyDic = clientProxyDic;
            _modelManager = modelManager;
        }

        public void Activate()
        {
            _modelManager.PlayerModelDic.Adding += OnAdding;
        }

        public void Deactivate()
        {
            _modelManager.PlayerModelDic.Adding -= OnAdding;
        }

        private void OnAdding(int playerId, IPlayerModel playerModel)
        {
            var random = new Random();
            var newCharacterExemplarId = random.NextExclude(_modelManager.CharacterModelDic.Keys);
            CreateCharacter(newCharacterExemplarId);
            playerModel.ControllableCharacterExemplarId = newCharacterExemplarId;
        }

        private void CreateCharacter(int newCharacterExemplarId)
        {
            var characterData = new CharacterData
            {
                Id = "",
                HealthPointData = new HealthPointData {MaxPoints = 100, Points = 100}
            };
            var characterModel = new CharacterModel(characterData);
            _modelManager.CharacterModelDic.Add(newCharacterExemplarId, characterModel);
        }
    }
}