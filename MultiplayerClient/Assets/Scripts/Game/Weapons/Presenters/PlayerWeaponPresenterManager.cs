using System.Collections.Generic;
using System.Linq;
using Game.Weapons.Views;
using Models.Characters;
using Models.Weapons;

namespace Game.Weapons.Presenters
{
    public class PlayerWeaponPresenterManager : IPresenter
    {
        private readonly IEnumerable<IPlayerWeaponView> _views;
        private readonly IEnumerable<IWeaponModel> _models;
        private readonly ICharacterModel _playerCharacterModel;
        private readonly List<IPresenter> _presenters = new List<IPresenter>();

        public PlayerWeaponPresenterManager(IEnumerable<IPlayerWeaponView> views, IEnumerable<IWeaponModel>models, ICharacterModel playerCharacterModel)
        {
            _views = views;
            _models = models;
            _playerCharacterModel = playerCharacterModel;

            InstantiatePresenters();
        }

        private void InstantiatePresenters()
        {
            var viewList = _views.ToList();
            var modelList = _models.ToList();

            for (int i = 0; i < viewList.Count; i++)
            {
                _presenters.Add(new PlayerWeaponPresenter(viewList[i],modelList[i], _playerCharacterModel));
            }
        }

        public void Activate()
        {
            _presenters.ForEach(p=> p.Activate());
        }

        public void Deactivate()
        {
            _presenters.ForEach(p=> p.Deactivate());
        }
    }
}