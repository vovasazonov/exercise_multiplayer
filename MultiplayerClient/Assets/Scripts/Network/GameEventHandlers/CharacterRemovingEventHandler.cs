using System.Collections.Generic;
using Models.Characters;

namespace Network.GameEventHandlers
{
    public class CharacterRemovingEventHandler : IGameEventHandler
    {
        private readonly IMutablePacket _recordPacket;
        private readonly ITrackableDictionary<int, ICharacterModel> _characterModelDic;

        public CharacterRemovingEventHandler(IMutablePacket recordPacket, ITrackableDictionary<int, ICharacterModel> characterModelDic)
        {
            _recordPacket = recordPacket;
            _characterModelDic = characterModelDic;
        }

        public void Activate()
        {
            _characterModelDic.Removing += OnRemoving;
        }
        
        public void Deactivate()
        {
            _characterModelDic.Removing -= OnRemoving;
        }
        
        private void OnRemoving(int characterId, ICharacterModel characterModel)
        {
            _recordPacket.Fill(GameCommandType.CharacterRemove);
            _recordPacket.Fill(characterId);
        }
    }
}