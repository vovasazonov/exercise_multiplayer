namespace Network.CommandHandlers
{
    public readonly struct SetControllablePlayerCommandHandler : ICommandHandler
    {
        private readonly ICustomPacket _packet;
        private readonly IModelManagerClient _modelManagerClient;

        public SetControllablePlayerCommandHandler(ICustomPacket packet, IModelManagerClient modelManagerClient)
        {
            _packet = packet;
            _modelManagerClient = modelManagerClient;
        }

        public void HandleCommand()
        {
            var controllablePlayerId = _packet.Pull<int>();
            _modelManagerClient.SetControllablePlayer(controllablePlayerId);
        }
    }
}