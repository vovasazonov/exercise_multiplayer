using System.Collections.Generic;
using System.Linq;
using Game.Weapons.Views;
using Models;
using Models.Characters;
using Models.Weapons;

namespace Game.Weapons.Presenters
{
    public class PlayerWeaponPresenters : IPresenter
    {
        private readonly IEnumerable<IPlayerWeaponView> _views;
        private readonly IExemplarsModel<IWeaponModel> _weaponModels;
        private readonly ICharacterModel _playerCharacterModel;
        private readonly List<IPresenter> _presenters = new List<IPresenter>();

        public PlayerWeaponPresenters(IEnumerable<IPlayerWeaponView> views, IExemplarsModel<IWeaponModel> weaponModels, ICharacterModel playerCharacterModel)
        {
            _views = views;
            _weaponModels = weaponModels;
            _playerCharacterModel = playerCharacterModel;

            InstantiatePresenters();
        }

        private void InstantiatePresenters()
        {
            var viewList = _views.ToList();
            var modelIdsList = _weaponModels.ExemplarModelDic.Keys.ToList();

            for (int i = 0; i < viewList.Count; i++)
            {
                _presenters.Add(new PlayerWeaponPresenter(viewList[i], modelIdsList[i], _weaponModels.ExemplarModelDic[modelIdsList[i]], _playerCharacterModel));
            }
        }

        public void Activate()
        {
            _presenters.ForEach(p => p.Activate());
        }

        public void Deactivate()
        {
            _presenters.ForEach(p => p.Deactivate());
        }
    }
}