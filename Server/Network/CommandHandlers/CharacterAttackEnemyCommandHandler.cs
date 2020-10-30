using Models;

namespace Network.CommandHandlers
{
    public readonly struct CharacterAttackEnemyCommandHandler : ICommandHandler
    {
        private readonly ICustomPacket _unprocessedReceivedPacket;
        private readonly IModelManager _modelManager;

        public CharacterAttackEnemyCommandHandler(ICustomPacket unprocessedReceivedPacket, IModelManager modelManager)
        {
            _unprocessedReceivedPacket = unprocessedReceivedPacket;
            _modelManager = modelManager;
        }

        public void HandleCommand()
        {
            var characterExemplarId = _unprocessedReceivedPacket.Pull<int>();
            var enemyExemplarId = _unprocessedReceivedPacket.Pull<int>();

            var isCharacterExist = _modelManager.CharacterModelDic.ContainsKey(characterExemplarId) && _modelManager.CharacterModelDic.ContainsKey(enemyExemplarId);
            if (isCharacterExist)
            {
                _modelManager.CharacterModelDic[characterExemplarId].Attack(_modelManager.CharacterModelDic[enemyExemplarId]);
            }
        }
    }
}