using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game;
using Network;
using Serialization;
using Server.Network.CommandHandlers;

namespace Server.Network
{
    public class ClientCommandsProcessor : IClientCommandsProcessor
    {
        private readonly IModelManager _modelManager;
        private readonly IDictionary<int, IClientProxy> _clientProxyDic;
        private readonly ISerializer _serializer;
        private readonly int _millisecondsTick;
        private bool _isRun;

        public ClientCommandsProcessor(IModelManager modelManager, IDictionary<int, IClientProxy> clientProxyDic, ISerializer serializer, int millisecondsTick)
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
                    while (clientProxy.UnprocessedCommands.Count > 0)
                    {
                        HandleCommand(clientProxy.UnprocessedCommands);
                    }
                }
            }
        }

        public void Stop()
        {
            _isRun = false;
        }

        private void HandleCommand(Queue<byte> unprocessedPacketWithCommands)
        {
            GameCommandType commandType = _serializer.Deserialize<GameCommandType>(unprocessedPacketWithCommands);
            ICommandHandler commandHandler;

            switch (commandType)
            {
                case GameCommandType.HitCharacter:
                    commandHandler = new HitCharacterCommandHandler(unprocessedPacketWithCommands, _modelManager, _clientProxyDic, _serializer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            commandHandler.HandleCommand();
        }
    }
}