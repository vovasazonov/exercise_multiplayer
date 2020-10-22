using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game;
using Network;
using Serialization;
using Server.Network.CommandHandlers;

namespace Server.Network
{
    public class ServerGameProcessor : IServerGameProcessor
    {
        private readonly IModelManager _modelManager;
        private readonly IDictionary<int, IClientProxy> _clientProxyDic;
        private readonly ISerializer _serializer;
        private readonly int _millisecondsTick;
        private bool _isRun;

        public ServerGameProcessor(IModelManager modelManager, IDictionary<int, IClientProxy> clientProxyDic, ISerializer serializer, int millisecondsTick)
        {
            _modelManager = modelManager;
            _clientProxyDic = clientProxyDic;
            _serializer = serializer;
            _millisecondsTick = millisecondsTick;
        }

        public async void Start()
        {
            _isRun = true;
            
            while (_isRun)
            {
                await Task.Delay(_millisecondsTick);

                foreach (var clientProxy in _clientProxyDic.Values)
                {
                    HandleCommands(clientProxy);
                }
            }
        }

        public void Stop()
        {
            _isRun = false;
        }
        
        private void HandleCommands(IClientProxy clientProxy)
        {
            while (clientProxy.UnprocessedCommands.Count > 0)
            {
                HandleCommand(clientProxy.UnprocessedCommands);
            }
        }
        
        private void HandleCommand(Queue<byte> unprocessedCommands)
        {
            GameCommandType commandType = _serializer.Deserialize<GameCommandType>(unprocessedCommands);
            ICommandHandler commandHandler;

            switch (commandType)
            {
                case GameCommandType.HitCharacter:
                    commandHandler = new HitCharacterCommandHandler(unprocessedCommands, _modelManager, _clientProxyDic, _serializer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            commandHandler.HandleCommand();
        }
    }
}