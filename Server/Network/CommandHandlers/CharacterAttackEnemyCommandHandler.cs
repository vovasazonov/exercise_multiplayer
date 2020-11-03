using Models;

namespace Network.CommandHandlers
{
    public readonly struct CharacterAttackEnemyCommandHandler : ICommandHandler
    {
        private readonly IMutablePacket _unprocessedReceivedPacket;
        private readonly IModelManager _modelManager;

        public CharacterAttackEnemyCommandHandler(IMutablePacket unprocessedReceivedPacket, IModelManager modelManager)
        {
            _unprocessedReceivedPacket = unprocessedReceivedPacket;
            _modelManager = modelManager;
        }

        public void HandleCommand()
        {
            var characterExemplarId = _unprocessedReceivedPacket.Pull<int>();
            var enemyExemplarId = _unprocessedReceivedPacket.Pull<int>();

            var isCharactersExist = _modelManager.CharacterModelDic.ContainsKey(characterExemplarId) && _modelManager.CharacterModelDic.ContainsKey(enemyExemplarId);
            var isCharacterHoldWeapon = _modelManager.CharacterModelDic[characterExemplarId].HealthPoint != null;
            if (isCharactersExist && isCharacterHoldWeapon)
            {
                _modelManager.CharacterModelDic[characterExemplarId].Attack(_modelManager.CharacterModelDic[enemyExemplarId]);
            }
        }
    }
}