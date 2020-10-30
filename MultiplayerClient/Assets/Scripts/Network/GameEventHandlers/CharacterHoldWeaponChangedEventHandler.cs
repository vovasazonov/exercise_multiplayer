using System;
using Models.Characters;

namespace Network.GameEventHandlers
{
    public struct CharacterHoldWeaponChangedEventHandler : IGameEventHandler
    {
        private readonly ICharacterModel _characterModel;
        private readonly ICustomPacket _recordPacket;
        private readonly int _characterExemplarId;

        public CharacterHoldWeaponChangedEventHandler(ICustomPacket recordPacket, int characterExemplarId, ICharacterModel characterModel)
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

        private void OnHoldWeaponChanged(object sender, EventArgs e)
        {
            _recordPacket.Fill(GameCommandType.HoldWeaponChanged);
            _recordPacket.Fill(_characterExemplarId);
            _recordPacket.Fill(_characterModel.HoldWeapon.Id);
        }
    }
}