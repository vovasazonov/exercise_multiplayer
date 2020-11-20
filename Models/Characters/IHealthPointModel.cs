using System;

namespace Models.Characters
{
    public interface IHealthPointModel
    {
        event EventHandler MaxPointsUpdated;
        event EventHandler PointsUpdated;
        
        uint MaxPoints { get; }
        uint Points { get; }

        void SetMaxPoints(uint value);
        void TakePoints(uint amount);
        void AddPoints(uint amount);
    }
}