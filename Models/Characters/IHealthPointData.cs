using System;

namespace Models.Characters
{
    public interface IHealthPointData
    {
        event EventHandler MaxPointsUpdated;
        event EventHandler PointsUpdated;
        uint MaxPoints { get; set; }
        uint Points { get; set; }
    }
}