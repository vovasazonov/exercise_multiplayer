using System.Collections.Generic;
using Models.Characters;

namespace Network.GameEventHandlers
{
    public class CharacterAddingEventHandler : IGameEventHandler
    {
        private readonly IDictionary<uint, IClientProxy> _clientProxyDic;
        private readonly ITrackableDictionary<int, ICharacterModel> _characterModelDic;

        public CharacterAddingEventHandler(IDictionary<uint, IClientProxy> clientProxyDic, ITrackableDictionary<int, ICharacterModel> characterModelDic)
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
        
        private void OnAdding(int characterExemplarId, ICharacterModel characterModel)
        {
            NotifyClients(characterExemplarId, characterModel);
        }

        private void NotifyClients(int characterExemplarId, ICharacterModel characterModel)
        {
            foreach (var clientProxy in _clientProxyDic.Values)
            {
                clientProxy.NotSentToClientPacket.Fill(GameCommandType.CharacterAdd);
                clientProxy.NotSentToClientPacket.Fill(characterExemplarId);
                clientProxy.NotSentToClientPacket.Fill(characterModel.Id);
                clientProxy.NotSentToClientPacket.Fill(characterModel.HealthPoint.MaxPoints);
                clientProxy.NotSentToClientPacket.Fill(characterModel.HealthPoint.Points);
            }
        }
    }
}