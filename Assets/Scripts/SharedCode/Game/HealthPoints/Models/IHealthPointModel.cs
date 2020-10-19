using System;

namespace Game.HealthPoints.Models
{
    public interface IHealthPointModel
    {
        event Action PointsChanged;
        
        int Points { get; set; }
    }
}