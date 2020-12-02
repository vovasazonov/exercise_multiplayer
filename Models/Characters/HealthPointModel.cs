﻿using System;

namespace Models.Characters
{
    public class HealthPointModel : IHealthPointModel
    {
        public event EventHandler MaxPointsUpdated;
        public event EventHandler PointsUpdated;
        
        private readonly IHealthPointData _data;

        public HealthPointModel(IHealthPointData data)
        {
            _data = data;
            _data.PointsUpdated += OnPointsUpdated;
            _data.MaxPointsUpdated += OnMaxPointsUpdated;
        }
        
        public uint MaxPoints
        {
            get => _data.MaxPoints;
            private set => _data.MaxPoints = value;
        }

        public uint Points
        {
            get => _data.Points;
            private set => _data.Points = value;
        }

        public void SetMaxPoints(uint value)
        {
            MaxPoints = value;
        }

        public void TakePoints(uint amount)
        {
            if (Points != 0)
            {
                if (Points >= amount)
                {
                    Points -= amount;
                }
                else
                {
                    Points = 0;
                }
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
            }
        }
        
        private void OnMaxPointsUpdated(object sender, EventArgs e)
        {
            CallMaxPointsUpdated();
        }

        private void OnPointsUpdated(object sender, EventArgs e)
        {
            CallPointsUpdated();
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