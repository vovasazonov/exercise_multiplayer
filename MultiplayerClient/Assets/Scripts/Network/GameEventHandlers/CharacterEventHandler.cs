using System.Collections.Generic;
using Models.Characters;

namespace Network.GameEventHandlers
{
    public class CharacterEventHandler : IGameEventHandler
    {
        private readonly IDataMutablePacket _recordPacket;
        private readonly ITrackableDictionary<int, ICharacterModel> _characterModelDic;
        private readonly IDictionary<int, CharacterEnemyAttackedEventHandler> _enemyAttackedEventHandlerDic = new Dictionary<int, CharacterEnemyAttackedEventHandler>();
        private readonly IDictionary<int, CharacterHoldWeaponChangedEventHandler> _characterHoldWeaponEventHandlerDic = new Dictionary<int, CharacterHoldWeaponChangedEventHandler>();

        public CharacterEventHandler(IDataMutablePacket recordPacket, ITrackableDictionary<int, ICharacterModel> characterModelDic)
        {
            _recordPacket = recordPacket;
            _characterModelDic = characterModelDic;

            InstantiateEventHandlers();
        }

        private void AddCharacterModelDicListeners()
        {
            _characterModelDic.Added += OnCharacterAdded;
            _characterModelDic.Removed += OnCharacterRemoved;
        }
        
        private void RemoveCharacterModelDicListeners()
        {
            _characterModelDic.Added -= OnCharacterAdded;
            _characterModelDic.Removed -= OnCharacterRemoved;
        }

        public void Activate()
        {
            AddCharacterModelDicListeners();
            
            foreach (var characterHpChangedEventHandler in _enemyAttackedEventHandlerDic.Values)
            {
                characterHpChangedEventHandler.Activate();
            }

            foreach (var characterHoldWeaponChangedEventHandler in _characterHoldWeaponEventHandlerDic.Values)
            {
                characterHoldWeaponChangedEventHandler.Activate();
            }
        }

        public void Deactivate()
        {
            RemoveCharacterModelDicListeners();
            
            foreach (var characterHpChangedEventHandler in _enemyAttackedEventHandlerDic.Values)
            {
                characterHpChangedEventHandler.Deactivate();
            }
            
            foreach (var characterHoldWeaponChangedEventHandler in _characterHoldWeaponEventHandlerDic.Values)
            {
                characterHoldWeaponChangedEventHandler.Deactivate();
            }
        }
        
        private void OnCharacterRemoved(int exemplarId, ICharacterModel characterModel)
        {
            _characterHoldWeaponEventHandlerDic[exemplarId].Deactivate();
            _characterHoldWeaponEventHandlerDic.Remove(exemplarId);
            
            _enemyAttackedEventHandlerDic[exemplarId].Deactivate();
            _enemyAttackedEventHandlerDic.Remove(exemplarId);
        }

        private void OnCharacterAdded(int exemplarId, ICharacterModel characterModel)
        {
            InstantiateCharacterHoldWeaponEventHandler(exemplarId, characterModel);
            _characterHoldWeaponEventHandlerDic[exemplarId].Activate();
            
            InstantiateEnemyAttackedEventHandler(exemplarId, characterModel);
            _enemyAttackedEventHandlerDic[exemplarId].Activate();
        }

        private void InstantiateEventHandlers()
        {
            foreach (var keyValuePair in _characterModelDic)
            {
                InstantiateEnemyAttackedEventHandler(keyValuePair.Key, keyValuePair.Value);
                InstantiateCharacterHoldWeaponEventHandler(keyValuePair.Key, keyValuePair.Value);
            }
        }
        
        private void InstantiateEnemyAttackedEventHandler(in int exemplarId, ICharacterModel characterModel)
        {
            _enemyAttackedEventHandlerDic[exemplarId] = new CharacterEnemyAttackedEventHandler(_recordPacket, exemplarId, characterModel, _characterModelDic);
        }
        
        private void InstantiateCharacterHoldWeaponEventHandler(in int exemplarId, ICharacterModel characterModel)
        {
            _characterHoldWeaponEventHandlerDic[exemplarId] = new CharacterHoldWeaponChangedEventHandler(_recordPacket, exemplarId, characterModel);
        }
    }
}