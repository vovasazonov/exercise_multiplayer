using HealthPoints.Models;
using Weapons.Models;

namespace Enemies
{
    public interface IEnemyModel
    {
        IHealthPointModel HealthPoint { get; }
        void HitMe(IWeaponModel weapon);
    }
}