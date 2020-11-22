using System;
using System.Collections.Generic;
using Game.Characters.Presenters;
using Game.Weapons.Presenters;

namespace Game
{
    public class GamePresenter : IPresenter
    {
        private readonly ViewManager _viewManager;
        private readonly IModelManagerClient _modelManagerClient;
        private readonly List<IPresenter> _presenters = new List<IPresenter>();

        public GamePresenter(ViewManager viewManager, IModelManagerClient modelManagerClient)
        {
            _viewManager = viewManager;
            _modelManagerClient = modelManagerClient;

            AddModelManagerClientListener();
        }

        private void AddModelManagerClientListener()
        {
            _modelManagerClient.Loaded += OnModelLoaded;
        }

        private void RemoveModelManagerClientListener()
        {
            _modelManagerClient.Loaded -= OnModelLoaded;
        }

        private void OnModelLoaded(object sender, EventArgs e)
        {
            RemoveModelManagerClientListener();
            InstantiatePresenters();
        }

        private void InstantiatePresenters()
        {
            var controllablePlayerModel = _modelManagerClient.ModelManager.PlayersModel.ExemplarModelDic[_modelManagerClient.ControllablePlayerExemplarId];
            var controllableCharacterModel = _modelManagerClient.ModelManager.CharactersModel.ExemplarModelDic[controllablePlayerModel.ControllableCharacterExemplarId];

            _presenters.Add(new EnemyCharacterPresenters(_viewManager.CharacterViewPooler, controllablePlayerModel, _modelManagerClient.ModelManager.CharactersModel.ExemplarModelDic));
            _presenters.Add(new PlayerWeaponPresenters(_viewManager.playerWeaponViewList, _modelManagerClient.ModelManager.WeaponsModel, controllableCharacterModel));
            _presenters.ForEach(p => p.Activate());
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