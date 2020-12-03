using System;

namespace Models.Characters
{
    public interface IHealthPointModel
    {
        event Action MaxPointsUpdated;
        event Action PointsUpdated;
        
        uint MaxPoints { get; }
        uint Points { get; }

        void SetMaxPoints(uint value);
        void TakePoints(uint amount);
        void AddPoints(uint amount);
    }
}