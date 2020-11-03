using Models;

namespace Network.CommandHandlers
{
    public readonly struct CharacterRemoveEnemyCommandHandler : ICommandHandler
    {
        private readonly IMutablePacket _unprocessedReceivedPacket;
        private readonly IModelManager _modelManager;

        public CharacterRemoveEnemyCommandHandler(IMutablePacket unprocessedReceivedPacket, IModelManager modelManager)
        {
            _unprocessedReceivedPacket = unprocessedReceivedPacket;
            _modelManager = modelManager;
        }

        public void HandleCommand()
        {
            var characterExemplarId = _unprocessedReceivedPacket.Pull<int>();

            _modelManager.CharacterModelDic.Remove(characterExemplarId);
        }
    }
}