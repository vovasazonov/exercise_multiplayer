namespace Network.DataHandlers.CommandDataHandlers
{
    public readonly struct CharacterAttackEnemyDataHandler : IDataHandler
    {
        private readonly IMutablePacket _unprocessedReceivedPacket;
        private readonly IModelManagerServer _modelManagerServer;

        public CharacterAttackEnemyDataHandler(IMutablePacket unprocessedReceivedPacket, IModelManagerServer modelManagerServer)
        {
            _unprocessedReceivedPacket = unprocessedReceivedPacket;
            _modelManagerServer = modelManagerServer;
        }

        public void HandleData()
        {
            var characterExemplarId = _unprocessedReceivedPacket.Pull<int>();
            var enemyExemplarId = _unprocessedReceivedPacket.Pull<int>();

            var isCharactersExist = _modelManagerServer.ModelManager.CharactersModel.ExemplarModelDic.ContainsKey(characterExemplarId) && _modelManagerServer.ModelManager.CharactersModel.ExemplarModelDic.ContainsKey(enemyExemplarId);
            var isCharacterHoldWeapon = _modelManagerServer.ModelManager.CharactersModel.ExemplarModelDic[characterExemplarId].HealthPoint != null;
            if (isCharactersExist && isCharacterHoldWeapon)
            {
                _modelManagerServer.ModelManager.CharactersModel.ExemplarModelDic[characterExemplarId].Attack(_modelManagerServer.ModelManager.CharactersModel.ExemplarModelDic[enemyExemplarId]);
            }
        }
    }
}