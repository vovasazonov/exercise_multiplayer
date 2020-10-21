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
            _enemyCharacterHealthPointPresenter = new CharacterHealthPointPresenter(viewManager.EnemyHealthText, modelManagerClient.CharacterModelDic.First().Value.HealthPoint);
            _weaponAttackPresenterManager = new WeaponAttackPresenterManager(viewManager.WeaponButtonList, modelManagerClient.WeaponModelDic.Values, modelManagerClient.CharacterModelDic.First().Value);
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