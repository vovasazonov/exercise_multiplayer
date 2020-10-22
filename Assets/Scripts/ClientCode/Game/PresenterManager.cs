using System.Collections.Generic;
using System.Linq;
using Game.HealthPoints.Presenters;
using Game.Weapons.Presenters;

namespace Game
{
    public class PresenterManager : IPresenter
    {
        private readonly List<IPresenter> _presenters = new List<IPresenter>();

        public PresenterManager(ViewManager viewManager, ModelManagerClient modelManagerClient)
        {
            var enemyModel = modelManagerClient.CharacterModelDic.First().Value;
            _presenters.Add(new CharacterHealthPointPresenter(viewManager.EnemyHealthText, enemyModel.HealthPoint));
            _presenters.Add(new WeaponAttackPresenterManager(viewManager.WeaponButtonList, modelManagerClient.WeaponModelDic.Values, enemyModel));
        }

        public void Activate()
        {
            _presenters.ForEach(p=>p.Activate());
        }

        public void Deactivate()
        {
            _presenters.ForEach(p=>p.Deactivate());
        }
    }
}