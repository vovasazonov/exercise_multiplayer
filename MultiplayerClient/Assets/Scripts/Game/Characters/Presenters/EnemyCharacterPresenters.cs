using System.Collections.Generic;
using Game.Characters.Views;
using Models;
using Models.Characters;

namespace Game.Characters.Presenters
{
    public class EnemyCharacterPresenters : IPresenter
    {
        private readonly IViewPooler<ICharacterView> _enemyCharacterViewPooler;
        private readonly IPlayerModel _controllablePlayerModel;
        private readonly ITrackableDictionary<int, ICharacterModel> _characterModelDic;
        private readonly Dictionary<ICharacterModel, (ICharacterView, IPresenter)> _modelViewPresenterDic = new Dictionary<ICharacterModel, (ICharacterView, IPresenter)>();

        public EnemyCharacterPresenters(IViewPooler<ICharacterView> enemyCharacterViewPooler, IPlayerModel controllablePlayerModel,
            ITrackableDictionary<int, ICharacterModel> characterModelDic)
        {
            _enemyCharacterViewPooler = enemyCharacterViewPooler;
            _controllablePlayerModel = controllablePlayerModel;
            _characterModelDic = characterModelDic;

            InstantiatePresenters();
        }

        private void AddEnemyCharacterModelDicListeners()
        {
            _characterModelDic.Added += OnCharacterModelAdded;
            _characterModelDic.Removed += OnCharacterModelRemoved;
        }

        private void RemoveEnemyCharacterModelDicListeners()
        {
            _characterModelDic.Added -= OnCharacterModelAdded;
            _characterModelDic.Removed -= OnCharacterModelRemoved;
        }

        private void OnCharacterModelRemoved(int idExemplar, ICharacterModel model)
        {
            RemovePresenter(model);
        }

        private void OnCharacterModelAdded(int idExemplar, ICharacterModel model)
        {
            InstantiatePresenter(model);
            _modelViewPresenterDic[model].Item2.Activate();
        }

        private void InstantiatePresenters()
        {
            foreach (var characterModel in _characterModelDic.Values)
            {
                InstantiatePresenter(characterModel);
            }
        }

        private void InstantiatePresenter(ICharacterModel model)
        {
            var view = _enemyCharacterViewPooler.GetView();
            var presenter = new EnemyCharacterPresenter(view, model, _characterModelDic[_controllablePlayerModel.ControllableCharacterExemplarId]);
            _modelViewPresenterDic[model] = (view, presenter);
        }

        private void RemovePresenter(ICharacterModel model)
        {
            var view = _modelViewPresenterDic[model].Item1;
            var presenter = _modelViewPresenterDic[model].Item2;
            presenter.Deactivate();
            _modelViewPresenterDic.Remove(model);
            _enemyCharacterViewPooler.ReturnView(view);
        }

        public void Activate()
        {
            AddEnemyCharacterModelDicListeners();
            foreach (var viewPresenter in _modelViewPresenterDic.Values)
            {
                var presenter = viewPresenter.Item2;
                presenter.Activate();
            }
        }

        public void Deactivate()
        {
            RemoveEnemyCharacterModelDicListeners();
            foreach (var viewPresenter in _modelViewPresenterDic.Values)
            {
                var presenter = viewPresenter.Item2;
                presenter.Deactivate();
            }
        }
    }
}