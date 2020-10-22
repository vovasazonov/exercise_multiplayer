using System.Collections.Generic;
using Game.HealthPoints.Presenters;
using Game.Weapons.Presenters;

namespace Game
{
    public class PresenterManager : IPresenter
    {
        private readonly List<IPresenter> _presenters = new List<IPresenter>();

        public PresenterManager(ViewManager viewManager, ModelManagerClient modelManagerClient)
        {
            _presenters.Add(new CharacterHealthPointPresenterManager(new[] {viewManager.EnemyHealthText}, modelManagerClient.CharacterModelDic.Values));
            _presenters.Add(new WeaponAttackPresenterManager(viewManager.WeaponButtonList, modelManagerClient.WeaponModelDic.Values, modelManagerClient.CharacterModelDic.Values));
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