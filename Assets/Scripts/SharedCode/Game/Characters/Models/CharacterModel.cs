using Game.HealthPoints.Models;
using Game.Weapons.Models;

namespace Game.Characters.Models
{
    public class CharacterModel : ICharacterModel
    {
        public int Id { get; }
        public IHealthPointModel HealthPoint { get; }

        public CharacterModel(int id, int healthPoint)
        {
            Id = id;
            HealthPoint = new HealthPointModel(healthPoint);
        }
        
        public void HitMe(IWeaponModel weapon)
        {
            HealthPoint.Points -= weapon.Damage;
        }
    }
}