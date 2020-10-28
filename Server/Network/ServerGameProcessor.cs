using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Network.CommandHandlers;

namespace Network
{
    public class ServerGameProcessor : IDisposable
    {
        private readonly IDictionary<uint, IClientProxy> _clientProxyDic;
        private int _millisecondsTick;
        private bool _isGameProcessLoop;
        private readonly Task _gameProcessorLoopTask;

        public int MillisecondsTick
        {
            set => _millisecondsTick = value;
        }

        public ServerGameProcessor(IDictionary<uint, IClientProxy> clientProxyDic)
        {
            _clientProxyDic = clientProxyDic;
            
            _isGameProcessLoop = true;
            _gameProcessorLoopTask = new Task(StartGameProcessLoop);
            _gameProcessorLoopTask.Start();
        }

        private void StartGameProcessLoop()
        {
            _isGameProcessLoop = true;
            
            while (_isGameProcessLoop)
            {
                Task.Delay(_millisecondsTick).Wait();
                
                foreach (var clientProxy in _clientProxyDic.Values)
                {
                    HandleUnprocessedCommands(clientProxy);
                }
            }
        }

        private void HandleUnprocessedCommands(IClientProxy clientProxy)
        {
            while (clientProxy.UnprocessedReceivedPacket.Data.Length > 0)
            {
                ICommandHandler commandHandler = new MainCommandHandler(clientProxy.UnprocessedReceivedPacket);
                commandHandler.HandleCommand();
            }
        }

        public void Dispose()
        {
            _isGameProcessLoop = false;
            _gameProcessorLoopTask.Wait();
        }
    }
}