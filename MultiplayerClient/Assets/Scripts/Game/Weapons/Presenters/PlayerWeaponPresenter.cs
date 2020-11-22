using Game.Weapons.Views;
using Models.Characters;
using Models.Weapons;

namespace Game.Weapons.Presenters
{
    public class PlayerWeaponPresenter : IPresenter
    {
        private readonly IPlayerWeaponView _view;
        private readonly int _weaponExemplarId;
        private readonly IWeaponModel _weaponExemplarModel;
        private readonly ICharacterModel _playerCharacterModel;

        public PlayerWeaponPresenter(IPlayerWeaponView view, int weaponExemplarId ,IWeaponModel weaponExemplarModel, ICharacterModel playerCharacterModel)
        {
            _view = view;
            _weaponExemplarId = weaponExemplarId;
            _weaponExemplarModel = weaponExemplarModel;
            _playerCharacterModel = playerCharacterModel;

            RenderView();
        }

        private void RenderView()
        {
            _view.DamageTextView.TextUi = _weaponExemplarModel.Damage.ToString();
        }

        public void Activate()
        {
            _view.Clicked += OnClicked;
        }

        public void Deactivate()
        {
            _view.Clicked -= OnClicked;
        }

        private void OnClicked()
        {
            ChangeHoldWeapon();
        }

        private void ChangeHoldWeapon()
        {
            _playerCharacterModel.ChangeHoldWeapon(_weaponExemplarId);
        }
    }
}