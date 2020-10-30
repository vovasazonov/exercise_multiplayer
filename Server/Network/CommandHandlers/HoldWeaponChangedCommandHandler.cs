using System.Linq;
using Models;

namespace Network.CommandHandlers
{
    public readonly struct HoldWeaponChangedCommandHandler : ICommandHandler
    {
        private readonly ICustomPacket _unprocessedReceivedPacket;
        private readonly IModelManager _modelManager;

        public HoldWeaponChangedCommandHandler(ICustomPacket unprocessedReceivedPacket, IModelManager modelManager)
        {
            _unprocessedReceivedPacket = unprocessedReceivedPacket;
            _modelManager = modelManager;
        }
        
        public void HandleCommand()
        {
            var characterExemplarId = _unprocessedReceivedPacket.Pull<int>();
            var weaponTypeId = _unprocessedReceivedPacket.Pull<string>();
            
            var weaponModel = _modelManager.WeaponModelForAllPlayersList.First(w => w.Id == weaponTypeId);
            _modelManager.CharacterModelDic[characterExemplarId].ChangeHoldWeapon(weaponModel);
        }
    }
}