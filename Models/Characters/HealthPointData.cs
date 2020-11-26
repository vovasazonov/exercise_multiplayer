using System;

namespace Models.Characters
{
    public sealed class HealthPointData : IHealthPointData
    {
        public event EventHandler MaxPointsUpdated;
        public event EventHandler PointsUpdated;
        
        private uint _maxPoints;
        private uint _points;

        public uint MaxPoints
        {
            get => _maxPoints;
            set
            {
                _maxPoints = value;
                OnMaxPointsUpdated();
            }
        }

        public uint Points
        {
            get => _points;
            set
            {
                _points = value;
                OnPointsUpdated();
            }
        }

        private void OnMaxPointsUpdated()
        {
            MaxPointsUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void OnPointsUpdated()
        {
            PointsUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}