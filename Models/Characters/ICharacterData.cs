namespace Models.Characters
{
    public interface ICharacterData
    {
        string Id { get; set; }
        IHealthPointData HealthPointData { get; set; }
        string HoldWeaponId { get; set; }
    }
}