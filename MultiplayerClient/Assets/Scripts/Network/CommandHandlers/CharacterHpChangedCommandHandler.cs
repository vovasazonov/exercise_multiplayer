using Models;

namespace Network.CommandHandlers
{
    public readonly struct CharacterHpChangedCommandHandler : ICommandHandler
    {
        private readonly ICustomPacket _packet;
        private readonly IModelManager _modelManager;

        public CharacterHpChangedCommandHandler(ICustomPacket packet, IModelManager modelManager)
        {
            _packet = packet;
            _modelManager = modelManager;
        }
        
        public void HandleCommand()
        {
            var characterExemplarId = _packet.Pull<int>();
            var currentHp = _packet.Pull<uint>();
            var healthPointModel = _modelManager.CharacterModelDic[characterExemplarId].HealthPoint;
            
            if (currentHp > healthPointModel.Points)
            {
                var amountAdd = currentHp - healthPointModel.Points;
                healthPointModel.AddPoints(amountAdd);
            }
            else
            {
                var amountTake = healthPointModel.Points - currentHp;
                healthPointModel.TakePoints(amountTake);
            }
        }
    }
}