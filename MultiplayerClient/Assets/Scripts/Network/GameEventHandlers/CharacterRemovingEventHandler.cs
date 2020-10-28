using System.Collections.Generic;
using Models.Characters;

namespace Network.GameEventHandlers
{
    public class CharacterRemovingEventHandler : IGameEventHandler
    {
        private readonly ICustomPacket _recordPacket;
        private readonly ITrackableDictionary<int, ICharacterModel> _characterModelDic;

        public CharacterRemovingEventHandler(ICustomPacket recordPacket, ITrackableDictionary<int, ICharacterModel> characterModelDic)
        {
            _recordPacket = recordPacket;
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
            _recordPacket.Fill(GameCommandType.CharacterRemove);
            _recordPacket.Fill(characterId);
        }
    }
}