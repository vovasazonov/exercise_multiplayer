using Models;
using Models.Characters;

namespace Network.CommandHandlers
{
    public readonly struct CharacterAddCommandHandler: ICommandHandler
    {
        private readonly ICustomPacket _packet;
        private readonly IModelManager _modelManager;

        public CharacterAddCommandHandler(ICustomPacket packet, IModelManager modelManager)
        {
            _packet = packet;
            _modelManager = modelManager;
        }

        public void HandleCommand()
        {
            var characterExemplarId = _packet.Pull<int>();
            var characterTypeId = _packet.Pull<string>();
            var maxPoints = _packet.Pull<uint>();
            var points = _packet.Pull<uint>();
            var characterModel = new CharacterModel(new CharacterData()
            {
                Id = characterTypeId,
                HealthPointData = new HealthPointData()
                {
                    MaxPoints = maxPoints,
                    Points = points
                }
            });

            if (!_modelManager.CharacterModelDic.ContainsKey(characterExemplarId))
            {
                _modelManager.CharacterModelDic.Add(characterExemplarId,characterModel);
            }
        }
    }
}