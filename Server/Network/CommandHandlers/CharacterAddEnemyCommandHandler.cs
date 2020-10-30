using Models;

namespace Network.CommandHandlers
{
    public readonly struct CharacterAddEnemyCommandHandler : ICommandHandler
    {
        private readonly ICustomPacket _unprocessedReceivedPacket;
        private readonly IModelManager _modelManager;

        public CharacterAddEnemyCommandHandler(ICustomPacket unprocessedReceivedPacket, IModelManager modelManager)
        {
            _unprocessedReceivedPacket = unprocessedReceivedPacket;
            _modelManager = modelManager;
        }

        public void HandleCommand()
        {
            var characterExemplarId = _unprocessedReceivedPacket.Pull<int>();
        }
    }
}