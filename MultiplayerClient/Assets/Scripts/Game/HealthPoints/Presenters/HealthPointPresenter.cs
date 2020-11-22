using System;
using Game.Views;
using Models.Characters;

namespace Game.HealthPoints.Presenters
{
    public class HealthPointPresenter : IPresenter
    {
        private readonly ITextUiView _view;
        private readonly IHealthPointModel _model;

        public HealthPointPresenter(ITextUiView view, IHealthPointModel model)
        {
            _view = view;
            _model = model;
        }

        public void Activate()
        {
            _model.PointsUpdated += OnPointsUpdated;
            Update();
        }

        public void Deactivate()
        {
            _model.PointsUpdated -= OnPointsUpdated;
        }

        private void OnPointsUpdated(object sender, EventArgs e)
        {
            _view.TextUi = _model.Points.ToString();
        }

        private void Update()
        {
            _view.TextUi = _model.Points.ToString();
        }
    }
}