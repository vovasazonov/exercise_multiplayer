using System;

namespace Replications
{
    public class HealthData : IHealthData
    {
        public event EventHandler CurrentPointsUpdated;

        private int _currentPoints;

        public int CurrentPoints
        {
            get => _currentPoints;
            set
            {
                if (_currentPoints != value)
                {
                    _currentPoints = value;
                    OnCurrentPointsUpdated();
                }
            }
        }

        private void OnCurrentPointsUpdated() => CurrentPointsUpdated?.Invoke(this, EventArgs.Empty);
    }
}