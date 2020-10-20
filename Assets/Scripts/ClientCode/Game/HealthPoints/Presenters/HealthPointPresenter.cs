using Game.HealthPoints.Models;
using Game.Views;

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

            RenderView();
        }

        private void RenderView()
        {
            _view.TextUi = 0.ToString();
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