using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Network.CommandHandlers;

namespace Network
{
    public class ServerGameProcessor : IDisposable
    {
        public event EventHandler Processed;
        
        private readonly IDictionary<uint, IClientProxy> _clientProxyDic;
        private readonly IModelManager _modelManager;
        private int _millisecondsTick;
        private bool _isGameProcessLoop;
        private readonly Task _gameProcessorLoopTask;

        public int MillisecondsTick
        {
            set => _millisecondsTick = value;
        }

        public ServerGameProcessor(IDictionary<uint, IClientProxy> clientProxyDic, IModelManager modelManager)
        {
            _clientProxyDic = clientProxyDic;
            _modelManager = modelManager;

            _isGameProcessLoop = true;
            _gameProcessorLoopTask = Task.Factory.StartNew(StartGameProcessLoop).ContinueWith(task=>Console.WriteLine(task.Exception),TaskContinuationOptions.OnlyOnFaulted);
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
                
                OnProcessed();
            }
        }

        private void HandleUnprocessedCommands(IClientProxy clientProxy)
        {
            while (clientProxy.UnprocessedReceivedPacket.Data.Length > 0)
            {
                ICommandHandler commandHandler = new MainCommandHandler(clientProxy.UnprocessedReceivedPacket, _modelManager);
                commandHandler.HandleCommand();
            }
        }

        private void OnProcessed()
        {
            Processed?.Invoke(this, EventArgs.Empty);
        }
        
        public void Dispose()
        {
            _isGameProcessLoop = false;
            _gameProcessorLoopTask.Wait();
        }
    }
}