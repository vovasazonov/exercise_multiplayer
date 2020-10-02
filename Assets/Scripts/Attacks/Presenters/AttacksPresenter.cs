using System.Collections.Generic;
using System.Linq;
using Attacks.Views;
using Descriptions;
using Enemies;
using Weapons.Models;

namespace Attacks.Presenters
{
    public class AttacksPresenter
    {
        private readonly List<AttackPresenter> _presenters = new List<AttackPresenter>();

        public AttacksPresenter(IEnumerable<IAttackButtonView> buttonViews, DescriptionsWeapon descriptionsWeapon, IEnumerable<IWeaponModel> weaponModels, IEnemyModel enemyModel)
        {
            var viewList = buttonViews.ToList();
            var weaponModelsList = weaponModels.ToList();
            for (int i = 0; i < descriptionsWeapon.Descriptions.Count; i++)
            {
                var attackPresenter = new AttackPresenter(viewList[i], weaponModelsList[i], enemyModel, descriptionsWeapon.Descriptions[i]);
                _presenters.Add(attackPresenter);
            }
        }
        
        public void Activate()
        {
            _presenters.ForEach(t=>t.Activate());
        }

        public void Deactivate()
        {
            _presenters.ForEach(t=>t.Deactivate());
        }
    }
}