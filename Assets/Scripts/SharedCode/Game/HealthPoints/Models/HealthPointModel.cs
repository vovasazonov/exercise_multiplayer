using System;

namespace Game.HealthPoints.Models
{
    public class HealthPointModel : IHealthPointModel
    {
        public event Action PointsChanged;

        private int _points = 0;

        public HealthPointModel(int points)
        {
            Points = points;
        }

        public int Points
        {
            get => _points;
            set
            {
                _points = value;
                OnPointsChanged();
            }
        }

        private void OnPointsChanged()
        {
            PointsChanged?.Invoke();
        }
    }
}