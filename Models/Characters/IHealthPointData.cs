using System;

namespace Models.Characters
{
    public interface IHealthPointData
    {
        event Action MaxPointsUpdated;
        event Action PointsUpdated;
        uint MaxPoints { get; set; }
        uint Points { get; set; }
    }
}