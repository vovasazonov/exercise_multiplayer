using Client;
using HealthPoints.Models;
using Weapons.Models;

namespace Enemies
{
    public class EnemyModel : IEnemyModel
    {
        private readonly IClient _client;
        public IHealthPointModel HealthPoint { get; }

        public EnemyModel(IClient client)
        {
            _client = client;
            HealthPoint = new HealthPointModel(_client, 100);
        }

        public void HitMe(IWeaponModel weapon)
        {
            HealthPoint.Take(weapon.Damage);
        }
    }
}