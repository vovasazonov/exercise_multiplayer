namespace Game.Weapons.Models
{
    public class WeaponModel : IWeaponModel
    {
        public string Id { get; }
        public int Damage { get; }

        public WeaponModel(string id, int damage)
        {
            Id = id;
            Damage = damage;
        }
    }
}