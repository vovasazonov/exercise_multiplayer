using System;
using System.Collections.Generic;
using Game.Characters.Presenters;
using Game.Weapons.Presenters;

namespace Game
{
    public class GameManagerPresenter : IPresenter
    {
        private readonly ViewManager _viewManager;
        private readonly IModelManagerClient _modelManagerClient;
        private readonly List<IPresenter> _presenters = new List<IPresenter>();

        public GameManagerPresenter(ViewManager viewManager, IModelManagerClient modelManagerClient)
        {
            _viewManager = viewManager;
            _modelManagerClient = modelManagerClient;
        }

        private void AddModelManagerClientListener()
        {
            _modelManagerClient.ControllablePlayerSet += OnControllablePlayerSet;
        }

        private void RemoveModelManagerClientListener()
        {
            _modelManagerClient.ControllablePlayerSet -= OnControllablePlayerSet;
        }

        private void OnControllablePlayerSet(object sender, EventArgs e)
        {
            InstantiatePresenters();
        }

        private void InstantiatePresenters()
        {
            var controllablePlayerModel = _modelManagerClient.ModelManager.PlayerModelDic[_modelManagerClient.ControllablePlayerExemplarId];
            var controllableCharacterModel = _modelManagerClient.ModelManager.CharacterModelDic[controllablePlayerModel.ControllableCharacterExemplarId];

            _presenters.Add(new EnemyCharacterPresenters(_viewManager.CharacterViewPooler, controllablePlayerModel, _modelManagerClient.ModelManager.CharacterModelDic));
            _presenters.Add(new PlayerWeaponPresenterManager(_viewManager.playerWeaponViewList, _modelManagerClient.ModelManager.GameWeaponModelDic.Values, controllableCharacterModel));
            _presenters.ForEach(p => p.Activate());
        }

        public void Activate()
        {
            _presenters.ForEach(p => p.Activate());
            AddModelManagerClientListener();
        }

        public void Deactivate()
        {
            RemoveModelManagerClientListener();
            _presenters.ForEach(p => p.Deactivate());
        }
    }
}