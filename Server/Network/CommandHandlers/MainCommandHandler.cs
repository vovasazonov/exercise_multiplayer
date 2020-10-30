using System;
using Models;

namespace Network.CommandHandlers
{
    public readonly struct MainCommandHandler : ICommandHandler
    {
        private readonly ICustomPacket _unprocessedReceivedPacket;
        private readonly IModelManager _modelManager;

        public MainCommandHandler(ICustomPacket unprocessedReceivedPacket, IModelManager modelManager)
        {
            _unprocessedReceivedPacket = unprocessedReceivedPacket;
            _modelManager = modelManager;
        }

        public void HandleCommand()
        {
            GameCommandType commandType = _unprocessedReceivedPacket.Pull<GameCommandType>();
            ICommandHandler commandHandler;

            switch (commandType)
            {
                case GameCommandType.CharacterAttackEnemy:
                    commandHandler = new CharacterAttackEnemyCommandHandler(_unprocessedReceivedPacket, _modelManager);
                    break;
                case GameCommandType.CharacterAdd:
                    commandHandler = new CharacterAddEnemyCommandHandler(_unprocessedReceivedPacket,_modelManager);
                    break;
                case GameCommandType.CharacterRemove:
                    commandHandler = new CharacterRemoveEnemyCommandHandler(_unprocessedReceivedPacket,_modelManager);
                    break;
                case GameCommandType.HoldWeaponChanged:
                    commandHandler = new HoldWeaponChangedCommandHandler(_unprocessedReceivedPacket,_modelManager);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            commandHandler.HandleCommand();
        }
    }
}