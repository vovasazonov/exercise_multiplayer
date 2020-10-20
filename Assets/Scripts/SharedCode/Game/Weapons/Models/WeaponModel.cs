namespace Game.Weapons.Models
{
    public class WeaponModel : IWeaponModel
    {
        public int Id { get; }
        public int Damage { get; }

        public WeaponModel(int id, int damage)
        {
            Id = id;
            Damage = damage;
        }
    }
}