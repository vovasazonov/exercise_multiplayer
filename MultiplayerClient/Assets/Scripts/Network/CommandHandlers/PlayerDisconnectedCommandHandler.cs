using Models;

namespace Network.CommandHandlers
{
    public readonly struct PlayerDisconnectedCommandHandler : ICommandHandler
    {
        private readonly ICustomPacket _packet;
        private readonly IModelManager _modelManager;

        public PlayerDisconnectedCommandHandler(ICustomPacket packet, IModelManager modelManager)
        {
            _packet = packet;
            _modelManager = modelManager;
        }

        public void HandleCommand()
        {
            int idPlayerDisconnected = _packet.Pull<int>();

            var disconnectedPlayerModel = _modelManager.PlayerModelDic[idPlayerDisconnected];
            _modelManager.CharacterModelDic.Remove(disconnectedPlayerModel.ControllableCharacterExemplarId);
            _modelManager.PlayerModelDic.Remove(idPlayerDisconnected);
        }
    }
}