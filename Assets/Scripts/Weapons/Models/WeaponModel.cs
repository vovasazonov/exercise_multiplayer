using Descriptions;

namespace Weapons.Models
{
    public class WeaponModel : IWeaponModel
    {
        public string Id { get; }
        public uint Damage { get; }
        
        public WeaponModel(DescriptionWeapon description)
        {
            Id = description.Id;
            Damage = description.Damage;
        }
    }
}