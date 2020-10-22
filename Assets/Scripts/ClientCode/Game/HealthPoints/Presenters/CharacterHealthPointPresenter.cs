using Game.HealthPoints.Models;
using Game.Views;

namespace Game.HealthPoints.Presenters
{
    public class CharacterHealthPointPresenter : IPresenter
    {
        private readonly ITextUiView _view;
        private readonly IHealthPointModel _model;

        public CharacterHealthPointPresenter(ITextUiView view, IHealthPointModel model)
        {
            _view = view;
            _model = model;
        }

        public void Activate()
        {
            _model.PointsChanged += OnPointsChanged;
            Update();
        }

        public void Deactivate()
        {
            _model.PointsChanged -= OnPointsChanged;
        }

        private void OnPointsChanged()
        {
            _view.TextUi = _model.Points.ToString();
        }

        private void Update()
        {
            _view.TextUi = _model.Points.ToString();
        }
    }
}