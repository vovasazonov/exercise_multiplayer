using Models.Weapons;

namespace Models.Characters
{
    public class CharactersModel : ExemplarsModel<ICharacterModel, ICharacterData>
    {
        private readonly IExemplarsModel<IWeaponModel> _weaponModels;

        public CharactersModel(IExemplarsData<ICharacterData> exemplarsData, IExemplarsModel<IWeaponModel> weaponModels) : base(exemplarsData)
        {
            _weaponModels = weaponModels;
        }

        protected override void AddModel(int id, ICharacterData data)
        {
            ExemplarModelDic.Add(id, new CharacterModel(data, _weaponModels));
        }
    }
}