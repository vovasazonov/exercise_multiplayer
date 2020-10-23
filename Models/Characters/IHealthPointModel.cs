using System;

namespace Models.Characters
{
    public interface IHealthPointModel
    {
        event EventHandler PointsChanged;
        
        uint MaxPoints { get; }
        uint Points { get; }

        void TakePoints(uint amount);
        void AddPoints(uint amount);
    }
}