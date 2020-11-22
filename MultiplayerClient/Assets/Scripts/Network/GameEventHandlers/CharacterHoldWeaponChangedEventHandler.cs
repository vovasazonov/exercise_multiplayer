using Models.Characters;

namespace Network.GameEventHandlers
{
    public struct CharacterHoldWeaponChangedEventHandler : IGameEventHandler
    {
        private readonly ICharacterModel _characterModel;
        private readonly IDataMutablePacket _recordPacket;
        private readonly int _characterExemplarId;

        public CharacterHoldWeaponChangedEventHandler(IDataMutablePacket recordPacket, int characterExemplarId, ICharacterModel characterModel)
        {
            _recordPacket = recordPacket;
            _characterExemplarId = characterExemplarId;
            _characterModel = characterModel;
        }

        public void Activate()
        {
            _characterModel.HoldWeaponChanged += OnHoldWeaponChanged;
        }

        public void Deactivate()
        {
            _characterModel.HoldWeaponChanged -= OnHoldWeaponChanged;
        }

        private void OnHoldWeaponChanged(object sender, WeaponChangedEventArgs e)
        {
            _recordPacket.MutablePacketDic[DataType.Command].Fill(GameCommandType.HoldWeaponChanged);
            _recordPacket.MutablePacketDic[DataType.Command].Fill(_characterExemplarId);
            _recordPacket.MutablePacketDic[DataType.Command].Fill(e.WeaponExemplarId);
        }
    }
}