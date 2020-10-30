using System;
using Network.CommandHandlers;

namespace Network.PacketHandlers
{
    public readonly struct CommandPacketHandler : IPacketHandler
    {
        private readonly ICustomPacket _packet;
        private readonly IModelManagerClient _modelManagerClient;

        public CommandPacketHandler(ICustomPacket packet, IModelManagerClient modelManagerClient)
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
                    case GameCommandType.PlayerDisconnected:
                        commandHandler = new PlayerDisconnectedCommandHandler(_packet, _modelManagerClient.ModelManager);
                        break;
                    case GameCommandType.PlayerConnected:
                        commandHandler = new PlayerConnectedCommandHandler(_packet, _modelManagerClient.ModelManager);
                        break;
                    case GameCommandType.CharacterHpChanged:
                        commandHandler = new CharacterHpChangedCommandHandler(_packet, _modelManagerClient.ModelManager);
                        break;
                    case GameCommandType.CharacterAdd:
                        commandHandler = new CharacterAddCommandHandler(_packet, _modelManagerClient.ModelManager);
                        break;
                    case GameCommandType.CharacterRemove:
                        commandHandler = new CharacterRemoveCommandHandler(_packet, _modelManagerClient.ModelManager);
                        break;
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