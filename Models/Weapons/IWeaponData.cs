namespace Models.Weapons
{
    public interface IWeaponData
    {
        string Id { get; set; }
        uint Damage { get; set; }
    }
}