using Models;
using Models.Characters;

namespace Network.CommandHandlers
{
    public readonly struct CharacterAddCommandHandler: ICommandHandler
    {
        private readonly IMutablePacket _packet;
        private readonly IModelManager _modelManager;

        public CharacterAddCommandHandler(IMutablePacket packet, IModelManager modelManager)
        {
            _packet = packet;
            _modelManager = modelManager;
        }

        public void HandleCommand()
        {
            var characterExemplarId = _packet.Pull<int>();
            var characterData = _packet.Pull<SerializableCharacterData>();

            if (!_modelManager.CharacterModelDic.ContainsKey(characterExemplarId))
            {
                var characterModel = new CharacterModel(characterData);
                _modelManager.CharacterModelDic.Add(characterExemplarId,characterModel);
            }
        }
    }
}