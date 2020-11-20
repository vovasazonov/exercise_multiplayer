namespace Models.Characters
{
    public class CharactersModel : ExemplarsModel<ICharacterModel,ICharacterData>
    {
        public CharactersModel(IExemplarsData<ICharacterData> exemplarsData) : base(exemplarsData)
        {
        }

        protected override void AddModel(int id, ICharacterData data)
        {
            ExemplarModelDic.Add(id,new CharacterModel(data));
        }
    }
}