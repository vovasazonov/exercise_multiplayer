namespace Models.Characters
{
    public interface ICharacterData
    {
        public string Id { get; set; }
        public IHealthPointData HealthPointData { get; set; }
        public string HoldWeaponId { get; set; }
    }
}