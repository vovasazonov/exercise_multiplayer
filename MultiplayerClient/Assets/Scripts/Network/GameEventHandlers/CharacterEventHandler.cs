using System.Collections.Generic;
using Models.Characters;

namespace Network.GameEventHandlers
{
    public class CharacterEventHandler : IGameEventHandler
    {
        private readonly ICustomPacket _recordPacket;
        private readonly ITrackableDictionary<int, ICharacterModel> _characterModelDic;
        private readonly IDictionary<int, CharacterEnemyAttackedEventHandler> _enemyAttackedEventHandlerDic = new Dictionary<int, CharacterEnemyAttackedEventHandler>();

        public CharacterEventHandler(ICustomPacket recordPacket, ITrackableDictionary<int, ICharacterModel> characterModelDic)
        {
            _recordPacket = recordPacket;
            _characterModelDic = characterModelDic;

            InstantiateEventHandlers();
        }

        private void AddCharacterModelDicListeners()
        {
            _characterModelDic.Adding += OnCharacterAdding;
            _characterModelDic.Removing += OnCharacterRemoving;
        }
        
        private void RemoveCharacterModelDicListeners()
        {
            _characterModelDic.Adding -= OnCharacterAdding;
            _characterModelDic.Removing -= OnCharacterRemoving;
        }

        public void Activate()
        {
            AddCharacterModelDicListeners();
            
            foreach (var characterHpChangedEventHandler in _enemyAttackedEventHandlerDic.Values)
            {
                characterHpChangedEventHandler.Activate();
            }
        }

        public void Deactivate()
        {
            RemoveCharacterModelDicListeners();
            
            foreach (var characterHpChangedEventHandler in _enemyAttackedEventHandlerDic.Values)
            {
                characterHpChangedEventHandler.Deactivate();
            }
        }
        
        private void OnCharacterRemoving(int exemplarId, ICharacterModel characterModel)
        {
            _enemyAttackedEventHandlerDic[exemplarId].Deactivate();
            _enemyAttackedEventHandlerDic.Remove(exemplarId);
        }

        private void OnCharacterAdding(int exemplarId, ICharacterModel characterModel)
        {
            InstantiateEnemyAttackedEventHandler(exemplarId, characterModel);
            _enemyAttackedEventHandlerDic[exemplarId].Activate();
        }

        private void InstantiateEventHandlers()
        {
            foreach (var keyValuePair in _characterModelDic)
            {
                _enemyAttackedEventHandlerDic[keyValuePair.Key] = new CharacterEnemyAttackedEventHandler(_recordPacket, keyValuePair.Key, keyValuePair.Value);
            }
        }
        
        private void InstantiateEnemyAttackedEventHandler(in int exemplarId, ICharacterModel characterModel)
        {
            _enemyAttackedEventHandlerDic[exemplarId] = new CharacterEnemyAttackedEventHandler(_recordPacket, exemplarId, characterModel, _characterModelDic);
        }
    }
}