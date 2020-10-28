using System.Collections.Generic;
using Models.Characters;

namespace Network.GameEventHandlers
{
    public class CharacterRemovingEventHandler : IGameEventHandler
    {
        private readonly IDictionary<uint, IClientProxy> _clientProxyDic;
        private readonly ITrackableDictionary<int, ICharacterModel> _characterModelDic;

        public CharacterRemovingEventHandler(IDictionary<uint, IClientProxy> clientProxyDic, ITrackableDictionary<int, ICharacterModel> characterModelDic)
        {
            _clientProxyDic = clientProxyDic;
            _characterModelDic = characterModelDic;
        }

        public void Activate()
        {
            _characterModelDic.Adding += OnAdding;
        }
        
        public void Deactivate()
        {
            _characterModelDic.Adding -= OnAdding;
        }
        
        private void OnAdding(int characterId, ICharacterModel characterModel)
        {
            foreach (var clientProxy in _clientProxyDic.Values)
            {
                clientProxy.NotSentToClientPacket.Fill(GameCommandType.CharacterRemove);
                clientProxy.NotSentToClientPacket.Fill(characterId);
            }
        }
    }
}