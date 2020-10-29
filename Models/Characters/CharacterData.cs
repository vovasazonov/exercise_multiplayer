namespace Models.Characters
{
    public class CharacterData : ICharacterData
    {
        public string Id { get; set; }
        public IHealthPointData HealthPointData { get; set; }
        public string HoldWeaponId { get; set; }
    }
}