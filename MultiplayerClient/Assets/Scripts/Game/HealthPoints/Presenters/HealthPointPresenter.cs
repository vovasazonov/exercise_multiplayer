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
            _model.PointsChanged += OnPointsChanged;
            Update();
        }

        public void Deactivate()
        {
            _model.PointsChanged -= OnPointsChanged;
        }

        private void OnPointsChanged(object sender, EventArgs e)
        {
            _view.TextUi = _model.Points.ToString();
        }

        private void Update()
        {
            _view.TextUi = _model.Points.ToString();
        }
    }
}