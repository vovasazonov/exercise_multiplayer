namespace Models.Weapons
{
    public class WeaponsModel : ExemplarsModel<IWeaponModel,IWeaponData>
    {
        public WeaponsModel(IExemplarsData<IWeaponData> exemplarsData) : base(exemplarsData)
        {
        }

        protected override void AddModel(int id, IWeaponData data)
        {
            ExemplarModelDic.Add(id,new WeaponModel(data));
        }
    }
}