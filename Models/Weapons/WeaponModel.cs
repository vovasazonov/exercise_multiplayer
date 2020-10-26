namespace Models.Weapons
{
    public class WeaponModel : IWeaponModel
    {
        public string Id { get; }
        public uint Damage { get; }
        
        public WeaponModel(string id, uint damage)
        {
            Id = id;
            Damage = damage;
        }
    }
}