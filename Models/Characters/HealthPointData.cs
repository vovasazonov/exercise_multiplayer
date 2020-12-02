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
                CallMaxPointsUpdated();
            }
        }

        public uint Points
        {
            get => _points;
            set
            {
                _points = value;
                CallPointsUpdated();
            }
        }

        private void CallMaxPointsUpdated()
        {
            MaxPointsUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void CallPointsUpdated()
        {
            PointsUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}