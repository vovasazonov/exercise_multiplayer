using Game.Weapons.Views;
using Models.Characters;
using Models.Weapons;

namespace Game.Weapons.Presenters
{
    public class PlayerWeaponPresenter : IPresenter
    {
        private readonly IPlayerWeaponView _view;
        private readonly IWeaponModel _weaponModel;
        private readonly ICharacterModel _playerCharacterModel;

        public PlayerWeaponPresenter(IPlayerWeaponView view, IWeaponModel weaponModel, ICharacterModel playerCharacterModel)
        {
            _view = view;
            _weaponModel = weaponModel;
            _playerCharacterModel = playerCharacterModel;

            RenderView();
        }

        private void RenderView()
        {
            _view.DamageTextView.TextUi = _weaponModel.Damage.ToString();
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
            _playerCharacterModel.ChangeHoldWeapon(_weaponModel);
        }
    }
}