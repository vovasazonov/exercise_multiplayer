using Attacks.Views;
using Descriptions;
using Enemies;
using Weapons.Models;

namespace Attacks.Presenters
{
    public class AttackPresenter
    {
        private readonly IAttackButtonView _view;
        private readonly IWeaponModel _weaponModel;
        private readonly IEnemyModel _enemyModel;
        private readonly DescriptionWeapon _descriptionWeapon;

        public AttackPresenter(IAttackButtonView view ,IWeaponModel weaponModel, IEnemyModel enemyModel, DescriptionWeapon descriptionWeapon)
        {
            _view = view;
            _weaponModel = weaponModel;
            _enemyModel = enemyModel;
            _descriptionWeapon = descriptionWeapon;

            RenderView();
        }

        private void RenderView()
        {
            _view.Damage = _descriptionWeapon.Damage.ToString();
        }

        public void Activate()
        {
            _view.Attack += OnAttack;
        }

        public void Deactivate()
        {
            _view.Attack -= OnAttack;
        }

        private void OnAttack()
        {
            _enemyModel.HitMe(_weaponModel);
        }
    }
}