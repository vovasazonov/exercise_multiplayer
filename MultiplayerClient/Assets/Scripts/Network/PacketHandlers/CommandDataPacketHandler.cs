using System;
using Network.CommandHandlers;

namespace Network.PacketHandlers
{
    public readonly struct CommandDataPacketHandler : IPacketHandler
    {
        private readonly IMutablePacket _packet;
        private readonly IModelManagerClient _modelManagerClient;

        public CommandDataPacketHandler(IMutablePacket packet, IModelManagerClient modelManagerClient)
        {
            _packet = packet;
            _modelManagerClient = modelManagerClient;
        }

        public void HandlePacket()
        {
            while (_packet.Data.Length > 0)
            {
                GameCommandType gameCommandType = _packet.Pull<GameCommandType>();
                ICommandHandler commandHandler;

                switch (gameCommandType)
                {
                    case GameCommandType.SetControllablePlayer:
                        commandHandler = new SetControllablePlayerCommandHandler(_packet, _modelManagerClient);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                commandHandler.HandleCommand();
            }
        }
    }
}