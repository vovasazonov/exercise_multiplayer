using Models;

namespace Network.CommandHandlers
{
    public readonly struct PlayerConnectedCommandHandler : ICommandHandler
    {
        private readonly ICustomPacket _packet;
        private readonly IModelManager _modelManager;

        public PlayerConnectedCommandHandler(ICustomPacket packet, IModelManager modelManager)
        {
            _packet = packet;
            _modelManager = modelManager;
        }

        public void HandleCommand()
        {
            int playerConnectedId = _packet.Pull<int>();
            int characterExemplarId = _packet.Pull<int>();

            var playerModel = new PlayerModel(_modelManager.CharacterModelDic);
            _modelManager.PlayerModelDic[playerConnectedId] = playerModel;
            playerModel.ControllableCharacterExemplarId = characterExemplarId;
        }
    }
}