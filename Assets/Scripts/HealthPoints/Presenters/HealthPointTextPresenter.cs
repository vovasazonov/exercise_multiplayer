using HealthPoints.Models;
using HealthPoints.Views;

namespace HealthPoints.Presenters
{
    public class HealthPointTextPresenter
    {
        private readonly IHealthPointTextView _view;
        private readonly IHealthPointModel _model;

        public HealthPointTextPresenter(IHealthPointTextView view, IHealthPointModel model)
        {
            _view = view;
            _model = model;
        }

        public void Activate()
        {
            _model.PointsChanged += OnPointsChanged;
        }

        public void Deactivate()
        {
            _model.PointsChanged -= OnPointsChanged;
        }

        private void OnPointsChanged()
        {
            _view.Points = _model.Points.ToString();
        }
    }
}