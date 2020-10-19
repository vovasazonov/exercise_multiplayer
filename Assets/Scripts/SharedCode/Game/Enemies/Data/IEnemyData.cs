using Game.HealthPoints.Data;

namespace Game.Enemies.Data
{
    public interface IEnemyData
    {
        int Id { get; set; }
        IHealthPointData HealthPoint { get; set; }
    }
}