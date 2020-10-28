using System.Collections.Generic;
using Models;

namespace Network.GameEventHandlers
{
    public class MainGameEventHandler : IGameEventHandler
    {
        private readonly ICustomPacket _recordPacket;
        private readonly IModelManager _modelManager;
        private readonly List<IGameEventHandler> _eventHandlers = new List<IGameEventHandler>();

        public MainGameEventHandler(ICustomPacket recordPacket, IModelManager modelManager)
        {
            _recordPacket = recordPacket;
            _modelManager = modelManager;

            InitializeGameEventHandlers();
        }

        private void InitializeGameEventHandlers()
        {
            _eventHandlers.Add(new CharacterAddingEventHandler(_recordPacket, _modelManager.CharacterModelDic));
            _eventHandlers.Add(new CharacterRemovingEventHandler(_recordPacket, _modelManager.CharacterModelDic));
            _eventHandlers.Add(new CharacterEventHandler(_recordPacket, _modelManager.CharacterModelDic));
        }

        public void Activate()
        {
            _eventHandlers.ForEach(e => e.Activate());
        }

        public void Deactivate()
        {
            _eventHandlers.ForEach(e => e.Deactivate());
        }
    }
}