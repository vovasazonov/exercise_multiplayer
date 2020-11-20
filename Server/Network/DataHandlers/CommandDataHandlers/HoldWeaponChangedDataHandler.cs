namespace Network.DataHandlers.CommandDataHandlers
{
    public readonly struct HoldWeaponChangedDataHandler : IDataHandler
    {
        private readonly IMutablePacket _unprocessedReceivedPacket;
        private readonly IGameManagerServer _gameManagerServer;

        public HoldWeaponChangedDataHandler(IMutablePacket unprocessedReceivedPacket, IGameManagerServer gameManagerServerServer)
        {
            _unprocessedReceivedPacket = unprocessedReceivedPacket;
            _gameManagerServer = gameManagerServerServer;
        }
        
        public void HandleData()
        {
            var characterExemplarId = _unprocessedReceivedPacket.Pull<int>();
            var weaponTypeId = _unprocessedReceivedPacket.Pull<int>();

            if (_gameManagerServer.ModelManager.WeaponsModel.ExemplarModelDic.ContainsKey(weaponTypeId))
            {
                var weaponModel = _gameManagerServer.ModelManager.WeaponsModel.ExemplarModelDic[weaponTypeId];
                _gameManagerServer.ModelManager.CharactersModel.ExemplarModelDic[characterExemplarId].ChangeHoldWeapon(weaponModel);
            }
        }
    }
}