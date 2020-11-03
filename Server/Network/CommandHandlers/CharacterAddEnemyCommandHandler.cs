using Models;
using Models.Characters;

namespace Network.CommandHandlers
{
    public readonly struct CharacterAddEnemyCommandHandler : ICommandHandler
    {
        private readonly IMutablePacket _unprocessedReceivedPacket;
        private readonly IModelManager _modelManager;

        public CharacterAddEnemyCommandHandler(IMutablePacket unprocessedReceivedPacket, IModelManager modelManager)
        {
            _unprocessedReceivedPacket = unprocessedReceivedPacket;
            _modelManager = modelManager;
        }

        public void HandleCommand()
        {
            var characterExemplarId = _unprocessedReceivedPacket.Pull<int>();
            var characterData = _unprocessedReceivedPacket.Pull<SerializableCharacterData>();
            
            if (!_modelManager.CharacterModelDic.ContainsKey(characterExemplarId))
            {
                var characterModel = new CharacterModel(characterData);
                _modelManager.CharacterModelDic.Add(characterExemplarId, characterModel);
            }
        }
    }
}