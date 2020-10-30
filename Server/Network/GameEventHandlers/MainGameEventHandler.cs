using System.Collections.Generic;
using Models;

namespace Network.GameEventHandlers
{
    public class MainGameEventHandler : IGameEventHandler
    {
        private readonly IDictionary<uint, IClientProxy> _clientProxyDic;
        private readonly IModelManager _modelManager;
        private readonly List<IGameEventHandler> _eventHandlers = new List<IGameEventHandler>();

        public MainGameEventHandler(IDictionary<uint, IClientProxy> clientProxyDic, IModelManager modelManager)
        {
            _clientProxyDic = clientProxyDic;
            _modelManager = modelManager;

            InitializeGameEventHandlers();
        }

        private void InitializeGameEventHandlers()
        {
            _eventHandlers.Add(new CharacterAddingEventHandler(_clientProxyDic,_modelManager.CharacterModelDic));
            _eventHandlers.Add(new CharacterRemovingEventHandler(_clientProxyDic,_modelManager.CharacterModelDic));
            _eventHandlers.Add(new MainCharacterEventHandler(_clientProxyDic,_modelManager.CharacterModelDic));
            _eventHandlers.Add(new PlayerAddingEventHandler(_clientProxyDic,_modelManager));
        }

        public void Activate()
        {
            _eventHandlers.ForEach(e=>e.Activate());
        }

        public void Deactivate()
        {
            _eventHandlers.ForEach(e=>e.Deactivate());
        }
    }
}