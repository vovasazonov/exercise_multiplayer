using Models;

namespace Network.CommandHandlers
{
    public readonly struct CharacterRemoveCommandHandler : ICommandHandler
    {
        private readonly IMutablePacket _packet;
        private readonly IModelManager _modelManager;

        public CharacterRemoveCommandHandler(IMutablePacket packet, IModelManager modelManager)
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