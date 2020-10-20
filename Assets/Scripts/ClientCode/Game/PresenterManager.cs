using Game.HealthPoints.Presenters;
using Game.Weapons.Presenters;

namespace Game
{
    public class PresenterManager : IPresenter
    {
        private readonly HealthPointPresenter _enemyHealthPointPresenter;
        private readonly WeaponAttackPresenterManager _weaponAttackPresenterManager;

        public PresenterManager(ViewManager viewManager, ModelManagerClient modelManagerClient)
        {
            _enemyHealthPointPresenter = new HealthPointPresenter(viewManager.EnemyHealthText, modelManagerClient.EnemyModel.HealthPoint);
            _weaponAttackPresenterManager = new WeaponAttackPresenterManager(viewManager.WeaponButtonList, modelManagerClient.ModelManager.WeaponModels, modelManagerClient.EnemyModel);
        }

        public void Activate()
        {
            _enemyHealthPointPresenter.Activate();
            _weaponAttackPresenterManager.Activate();
        }

        public void Deactivate()
        {
            _enemyHealthPointPresenter.Deactivate();
            _weaponAttackPresenterManager.Deactivate();
        }
    }
}