using Game.HealthPoints.Models;
using Game.Weapons.Models;

namespace Game.Characters.Models
{
    public interface ICharacterModel
    {
        string Id { get; }
        IHealthPointModel HealthPoint { get; }
        
        void HitMe(IWeaponModel weapon);
    }
}