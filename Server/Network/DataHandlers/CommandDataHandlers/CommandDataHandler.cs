using System;

namespace Network.DataHandlers.CommandDataHandlers
{
    public readonly struct CommandDataHandler : IDataHandler
    {
        private readonly IMutablePacket _unprocessedReceivedPacket;
        private readonly IGameManagerServer _gameManagerServer;

        public CommandDataHandler(IMutablePacket unprocessedReceivedPacket, IGameManagerServer gameManagerServer)
        {
            _unprocessedReceivedPacket = unprocessedReceivedPacket;
            _gameManagerServer = gameManagerServer;
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
                        dataHandler = new CharacterAttackEnemyDataHandler(_unprocessedReceivedPacket, _gameManagerServer);
                        break;
                    case GameCommandType.HoldWeaponChanged:
                        dataHandler = new HoldWeaponChangedDataHandler(_unprocessedReceivedPacket, _gameManagerServer);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                dataHandler.HandleData();
            }
        }
    }
}