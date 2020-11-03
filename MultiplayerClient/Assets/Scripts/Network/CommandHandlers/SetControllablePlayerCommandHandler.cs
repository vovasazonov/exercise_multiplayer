namespace Network.CommandHandlers
{
    public readonly struct SetControllablePlayerCommandHandler : ICommandHandler
    {
        private readonly IMutablePacket _packet;
        private readonly IModelManagerClient _modelManagerClient;

        public SetControllablePlayerCommandHandler(IMutablePacket packet, IModelManagerClient modelManagerClient)
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