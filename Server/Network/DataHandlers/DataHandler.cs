using System;
using Network.DataHandlers.CommandDataHandlers;

namespace Network.DataHandlers
{
    public readonly struct DataHandler : IDataHandler
    {
        private readonly IDataMutablePacket _unprocessedReceivedPacket;
        private readonly IModelManagerServer _modelManagerServer;

        public DataHandler(IDataMutablePacket unprocessedReceivedPacket, IModelManagerServer modelManagerServer)
        {
            _unprocessedReceivedPacket = unprocessedReceivedPacket;
            _modelManagerServer = modelManagerServer;
        }

        public void HandleData()
        {
            foreach (var dataType in _unprocessedReceivedPacket.MutablePacketDic.Keys)
            {
                var mutablePacket = _unprocessedReceivedPacket.MutablePacketDic[dataType];
                if (mutablePacket.Data.Length>0)
                {
                    IDataHandler dataHandler;
                    
                    switch (dataType)
                    {
                        case DataType.Command:
                            dataHandler = new CommandDataHandler(mutablePacket,_modelManagerServer);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    
                    dataHandler.HandleData();
                    
                }
            }
        }
    }
}