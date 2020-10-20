using Game.Characters.Models;
using Game.Weapons.Models;
using Game.Weapons.Views;

namespace Game.Weapons.Presenters
{
    public class WeaponAttackPresenter : IPresenter
    {
        private readonly IWeaponButtonView _view;
        private readonly IWeaponModel _weaponModel;
        private readonly ICharacterModel _characterToAttackModel;

        public WeaponAttackPresenter(IWeaponButtonView view, IWeaponModel weaponModel, ICharacterModel characterToAttackModel)
        {
            _view = view;
            _weaponModel = weaponModel;
            _characterToAttackModel = characterToAttackModel;

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
            _characterToAttackModel.HitMe(_weaponModel);
        }
    }
}