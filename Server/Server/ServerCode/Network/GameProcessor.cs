using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game;
using Network;
using Serialization;
using Server.Network.Commands;

namespace Server.Network
{
    public class GameProcessor
    {
        private readonly ModelManager _modelManager;
        private readonly Dictionary<int, ClientProxy> _clientProxyDic;
        private readonly ISerializer _serializer;
        private readonly int _millisecondsTick = 500;

        public GameProcessor(ModelManager modelManager, Dictionary<int, ClientProxy> clientProxyDic, ISerializer serializer)
        {
            _modelManager = modelManager;
            _clientProxyDic = clientProxyDic;
            _serializer = serializer;

            StartProcessGame();
        }

        private async void StartProcessGame()
        {
            while (true)
            {
                await Task.Delay(_millisecondsTick);

                foreach (var clientProxy in _clientProxyDic.Values)
                {
                    while (clientProxy.UnprocessedCommand.Count > 0)
                    {
                        HandleCommand(clientProxy.UnprocessedCommand, clientProxy.IdClient);
                    }
                }
            }
        }

        private void HandleCommand(Queue<byte> packet, int clientId)
        {
            GameCommandType commandType = _serializer.Deserialize<GameCommandType>(packet);
            IHandleCommand handleCommand;

            switch (commandType)
            {
                case GameCommandType.HitCharacter:
                    handleCommand = new HitCharacterHandleCommand(packet, _modelManager, _clientProxyDic, clientId, _serializer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            handleCommand.HandleCommand();
        }
    }
}