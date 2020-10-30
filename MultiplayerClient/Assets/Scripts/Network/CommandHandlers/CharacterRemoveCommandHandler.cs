using Models;

namespace Network.CommandHandlers
{
    public readonly struct CharacterRemoveCommandHandler : ICommandHandler
    {
        private readonly ICustomPacket _packet;
        private readonly IModelManager _modelManager;

        public CharacterRemoveCommandHandler(ICustomPacket packet, IModelManager modelManager)
        {
            _packet = packet;
            _modelManager = modelManager;
        }

        public void HandleCommand()
        {
            var characterExemplarId = _packet.Pull<int>();
            _modelManager.CharacterModelDic.Remove(characterExemplarId);
        }
    }
}