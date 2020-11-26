using System.Collections.Generic;

namespace Models.Weapons
{
    public class WeaponsModel : ExemplarsModel<IWeaponModel, IWeaponData>
    {
        public WeaponsModel(ITrackableDictionary<int,IWeaponData> exemplarsData) : base(exemplarsData)
        {
        }

        protected override void AddModel(int id, IWeaponData data)
        {
            ExemplarModelDic.Add(id, new WeaponModel(data));
        }
    }
}