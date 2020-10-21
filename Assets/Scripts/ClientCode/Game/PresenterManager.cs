using System.Linq;
using Game.HealthPoints.Presenters;
using Game.Weapons.Presenters;

namespace Game
{
    public class PresenterManager : IPresenter
    {
        private readonly CharacterHealthPointPresenter _enemyCharacterHealthPointPresenter;
        private readonly WeaponAttackPresenterManager _weaponAttackPresenterManager;

        public PresenterManager(ViewManager viewManager, ModelManagerClient modelManagerClient)
        {
            var enemyModel = modelManagerClient.CharacterModelDic.First().Value;
            _enemyCharacterHealthPointPresenter = new CharacterHealthPointPresenter(viewManager.EnemyHealthText, enemyModel.HealthPoint);
            _weaponAttackPresenterManager = new WeaponAttackPresenterManager(viewManager.WeaponButtonList, modelManagerClient.WeaponModelDic.Values, enemyModel);
        }

        public void Activate()
        {
            _enemyCharacterHealthPointPresenter.Activate();
            _weaponAttackPresenterManager.Activate();
        }

        public void Deactivate()
        {
            _enemyCharacterHealthPointPresenter.Deactivate();
            _weaponAttackPresenterManager.Deactivate();
        }
    }
}