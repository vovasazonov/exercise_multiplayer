using System;

namespace Network.DataHandlers.CommandDataHandlers
{
    public readonly struct CommandDataHandler : IDataHandler
    {
        private readonly IMutablePacket _unprocessedReceivedPacket;
        private readonly IModelManagerServer _modelManagerServer;

        public CommandDataHandler(IMutablePacket unprocessedReceivedPacket, IModelManagerServer modelManagerServer)
        {
            _unprocessedReceivedPacket = unprocessedReceivedPacket;
            _modelManagerServer = modelManagerServer;
        }
        
        public void HandleData()
        {
            while (_unprocessedReceivedPacket.Data.Length > 0)
            {
                IDataHandler dataHandler;
                GameCommandType commandType = _unprocessedReceivedPacket.Pull<GameCommandType>();
                
                switch (commandType)
                {
                    case GameCommandType.CharacterAttackEnemy:
                        dataHandler = new CharacterAttackEnemyDataHandler(_unprocessedReceivedPacket, _modelManagerServer);
                        break;
                    case GameCommandType.HoldWeaponChanged:
                        dataHandler = new HoldWeaponChangedDataHandler(_unprocessedReceivedPacket, _modelManagerServer);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                dataHandler.HandleData();
            }
        }
    }
}