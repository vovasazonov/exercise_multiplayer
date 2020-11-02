namespace Models.Characters
{
    public static class CharacterDataExtension
    {
        public static void Set(this ICharacterData data, ICharacterModel model)
        {
            data.Id = model.Id;
            var healthPointData = new HealthPointData();
            healthPointData.Set(model.HealthPoint);
            data.HealthPointData = healthPointData;
            data.HoldWeaponId = model.HoldWeapon?.Id;
        }
    }
}