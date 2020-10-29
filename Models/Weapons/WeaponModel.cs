namespace Models.Weapons
{
    public class WeaponModel : IWeaponModel
    {
        private readonly IWeaponData _data;
        public string Id => _data.Id;
        public uint Damage => _data.Damage;
        
        public WeaponModel(IWeaponData data)
        {
            _data = data;
        }
    }
}