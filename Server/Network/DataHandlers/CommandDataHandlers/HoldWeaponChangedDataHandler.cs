namespace Network.DataHandlers.CommandDataHandlers
{
    public readonly struct HoldWeaponChangedDataHandler : IDataHandler
    {
        private readonly IMutablePacket _unprocessedReceivedPacket;
        private readonly IModelManagerServer _modelManagerServer;

        public HoldWeaponChangedDataHandler(IMutablePacket unprocessedReceivedPacket, IModelManagerServer modelManagerServerServer)
        {
            _unprocessedReceivedPacket = unprocessedReceivedPacket;
            _modelManagerServer = modelManagerServerServer;
        }
        
        public void HandleData()
        {
            var characterExemplarId = _unprocessedReceivedPacket.Pull<int>();
            var weaponExemplarId = _unprocessedReceivedPacket.Pull<int>();

            if (_modelManagerServer.ModelManager.WeaponsModel.ExemplarModelDic.ContainsKey(weaponExemplarId))
            {
                _modelManagerServer.ModelManager.CharactersModel.ExemplarModelDic[characterExemplarId].ChangeHoldWeapon(weaponExemplarId);
            }
        }
    }
}