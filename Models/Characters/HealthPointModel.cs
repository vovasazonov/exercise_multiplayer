using System;

namespace Models.Characters
{
    public class HealthPointModel : IHealthPointModel
    {
        private readonly IHealthPointData _data;
        public event EventHandler PointsChanged;

        public HealthPointModel(IHealthPointData data)
        {
            _data = data;
        }

        public uint MaxPoints => _data.MaxPoints;

        public uint Points
        {
            get => _data.Points;
            private set => _data.Points = value;
        }

        public void TakePoints(uint amount)
        {
            if (Points != 0)
            {
                if (Points >= amount)
                {
                    Points -= amount;
                    OnPointsChanged();
                }
                else
                {
                    Points = 0;
                }

                OnPointsChanged();
            }
        }

        public void AddPoints(uint amount)
        {
            if (amount != 0 && Points != MaxPoints)
            {
                bool isResultBiggerMaxValue = uint.MaxValue - amount < Points;
                bool isResultBiggerMaxPoints = !isResultBiggerMaxValue && Points + amount > MaxPoints;

                if (isResultBiggerMaxValue || isResultBiggerMaxPoints)
                {
                    Points = MaxPoints;
                }
                else
                {
                    Points += amount;
                }

                OnPointsChanged();
            }
        }

        private void OnPointsChanged()
        {
            PointsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}