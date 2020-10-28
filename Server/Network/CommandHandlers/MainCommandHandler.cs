using System;

namespace Network.CommandHandlers
{
    public readonly struct MainCommandHandler : ICommandHandler
    {
        private readonly ICustomPacket _unprocessedReceivedPacket;

        public MainCommandHandler(ICustomPacket unprocessedReceivedPacket)
        {
            _unprocessedReceivedPacket = unprocessedReceivedPacket;
        }

        public void HandleCommand()
        {
            GameCommandType commandType = _unprocessedReceivedPacket.Pull<GameCommandType>();
            ICommandHandler commandHandler;

            switch (commandType)
            {
                default:
                    throw new ArgumentOutOfRangeException();
            }

            commandHandler.HandleCommand();
        }
    }
}