using Models;

namespace Network.CommandHandlers
{
    public readonly struct HoldWeaponChangedCommandHandler : ICommandHandler
    {
        private readonly IMutablePacket _unprocessedReceivedPacket;
        private readonly IModelManager _modelManager;

        public HoldWeaponChangedCommandHandler(IMutablePacket unprocessedReceivedPacket, IModelManager modelManager)
        {
            _unprocessedReceivedPacket = unprocessedReceivedPacket;
            _modelManager = modelManager;
        }
        
        public void HandleCommand()
        {
            var characterExemplarId = _unprocessedReceivedPacket.Pull<int>();
            var weaponTypeId = _unprocessedReceivedPacket.Pull<string>();

            if (_modelManager.GameWeaponModelDic.ContainsKey(weaponTypeId))
            {
                var weaponModel = _modelManager.GameWeaponModelDic[weaponTypeId];
                _modelManager.CharacterModelDic[characterExemplarId].ChangeHoldWeapon(weaponModel);
            }
        }
    }
}