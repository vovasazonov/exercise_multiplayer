using System.Collections.Generic;
using System.Linq;
using Game.Characters.Models;
using Game.Weapons.Models;
using Game.Weapons.Views;

namespace Game.Weapons.Presenters
{
    public class WeaponAttackPresenterManager : IPresenter
    {
        private readonly IEnumerable<IWeaponButtonView> _views;
        private readonly IEnumerable<IWeaponModel> _models;
        private readonly ICharacterModel _characterToAttackModel;
        private readonly List<IPresenter> _presenters = new List<IPresenter>();

        public WeaponAttackPresenterManager(IEnumerable<IWeaponButtonView> views, IEnumerable<IWeaponModel> models, ICharacterModel characterToAttackModel)
        {
            _views = views;
            _models = models;
            _characterToAttackModel = characterToAttackModel;

            InstantiatePresenters();
        }

        private void InstantiatePresenters()
        {
            var viewList = _views.ToList();
            var modelList = _models.ToList();

            for (int i = 0; i < viewList.Count; i++)
            {
                var presenter = new WeaponAttackPresenter(viewList[i], modelList[i], _characterToAttackModel);
                _presenters.Add(presenter);
            }
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