namespace Network.DataHandlers.CommandDataHandlers
{
    public readonly struct CharacterAttackEnemyDataHandler : IDataHandler
    {
        private readonly IMutablePacket _unprocessedReceivedPacket;
        private readonly IGameManagerServer _gameManagerServer;

        public CharacterAttackEnemyDataHandler(IMutablePacket unprocessedReceivedPacket, IGameManagerServer gameManagerServer)
        {
            _unprocessedReceivedPacket = unprocessedReceivedPacket;
            _gameManagerServer = gameManagerServer;
        }

        public void HandleData()
        {
            var characterExemplarId = _unprocessedReceivedPacket.Pull<int>();
            var enemyExemplarId = _unprocessedReceivedPacket.Pull<int>();

            var isCharactersExist = _gameManagerServer.ModelManager.CharactersModel.ExemplarModelDic.ContainsKey(characterExemplarId) && _gameManagerServer.ModelManager.CharactersModel.ExemplarModelDic.ContainsKey(enemyExemplarId);
            var isCharacterHoldWeapon = _gameManagerServer.ModelManager.CharactersModel.ExemplarModelDic[characterExemplarId].HealthPoint != null;
            if (isCharactersExist && isCharacterHoldWeapon)
            {
                _gameManagerServer.ModelManager.CharactersModel.ExemplarModelDic[characterExemplarId].Attack(_gameManagerServer.ModelManager.CharactersModel.ExemplarModelDic[enemyExemplarId]);
            }
        }
    }
}