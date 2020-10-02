using System;

namespace HealthPoints.Models
{
    public interface IHealthPointModel
    {
        event Action PointsChanged;
        uint Points { get; }
        void Take(uint points);
    }
}