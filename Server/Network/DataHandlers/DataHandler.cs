using System;
using Network.DataHandlers.CommandDataHandlers;

namespace Network.DataHandlers
{
    public readonly struct DataHandler : IDataHandler
    {
        private readonly IMutablePacket _unprocessedReceivedPacket;
        private readonly IGameManagerServer _gameManagerServer;

        public DataHandler(IMutablePacket unprocessedReceivedPacket, IGameManagerServer gameManagerServer)
        {
            _unprocessedReceivedPacket = unprocessedReceivedPacket;
            _gameManagerServer = gameManagerServer;
        }

        public void HandleData()
        {
            DataType dataType = _unprocessedReceivedPacket.Pull<DataType>();
            IDataHandler dataHandler;

            switch (dataType)
            {
                case DataType.Command:
                    dataHandler = new CommandDataHandler(_unprocessedReceivedPacket, _gameManagerServer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            dataHandler.HandleData();
        }
    }
}