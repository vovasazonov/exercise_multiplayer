using System.Collections.Generic;
using System.Linq;
using Game.Characters.Models;
using Game.Views;

namespace Game.HealthPoints.Presenters
{
    public class CharacterHealthPointPresenterManager : IPresenter
    {
        private readonly IEnumerable<ITextUiView> _views;
        private readonly IEnumerable<ICharacterModel> _models;
        private readonly List<IPresenter> _presenters = new List<IPresenter>();

        public CharacterHealthPointPresenterManager(IEnumerable<ITextUiView> views, IEnumerable<ICharacterModel> models)
        {
            _views = views;
            _models = models;
            
            InstantiatePresenters();
        }

        private void InstantiatePresenters()
        {
            var viewList = _views.ToList();
            var modelList = _models.ToList();

            for (int i = 0; i < viewList.Count; i++)
            {
                var presenter = new CharacterHealthPointPresenter(viewList[i], modelList[i].HealthPoint);
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