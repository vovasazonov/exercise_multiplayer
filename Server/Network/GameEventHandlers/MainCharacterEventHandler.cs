using System.Collections.Generic;
using Models.Characters;

namespace Network.GameEventHandlers
{
    public class MainCharacterEventHandler : IGameEventHandler
    {
        private readonly IDictionary<uint, IClientProxy> _clientProxyDic;
        private readonly ITrackableDictionary<int, ICharacterModel> _characterModelDic;
        private readonly IDictionary<int, HpChangedCharacterEventHandler> _characterHpChangedEventHandlerDic = new Dictionary<int, HpChangedCharacterEventHandler>();

        public MainCharacterEventHandler(IDictionary<uint, IClientProxy> clientProxyDic, ITrackableDictionary<int, ICharacterModel> characterModelDic)
        {
            _clientProxyDic = clientProxyDic;
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
            
            foreach (var characterHpChangedEventHandler in _characterHpChangedEventHandlerDic.Values)
            {
                characterHpChangedEventHandler.Activate();
            }
        }

        public void Deactivate()
        {
            RemoveCharacterModelDicListeners();
            
            foreach (var characterHpChangedEventHandler in _characterHpChangedEventHandlerDic.Values)
            {
                characterHpChangedEventHandler.Deactivate();
            }
        }
        
        private void OnCharacterRemoving(int exemplarId, ICharacterModel characterModel)
        {
            _characterHpChangedEventHandlerDic[exemplarId].Deactivate();
            _characterHpChangedEventHandlerDic.Remove(exemplarId);
        }

        private void OnCharacterAdding(int exemplarId, ICharacterModel characterModel)
        {
            InstantiateCharacterHpChangedEventHandler(exemplarId, characterModel);
            _characterHpChangedEventHandlerDic[exemplarId].Activate();
        }

        private void InstantiateEventHandlers()
        {
            foreach (var keyValuePair in _characterModelDic)
            {
                InstantiateCharacterHpChangedEventHandler(keyValuePair.Key, keyValuePair.Value);
            }
        }
        
        private void InstantiateCharacterHpChangedEventHandler(in int exemplarId, ICharacterModel characterModel)
        {
            _characterHpChangedEventHandlerDic[exemplarId] = new HpChangedCharacterEventHandler(_clientProxyDic, exemplarId, characterModel);
        }
    }
}