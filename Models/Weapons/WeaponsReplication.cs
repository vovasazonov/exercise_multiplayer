using System.Collections.Generic;
using Serialization;

namespace Models.Weapons
{
    public sealed class WeaponsReplication : ExemplarsReplication<IWeaponData>
    {
        public WeaponsReplication(ITrackableDictionary<int, IWeaponData> exemplarsData, ICustomCastObject castObject) : base(exemplarsData, castObject)
        {
        }

        protected override void InstantiateReplication(int exemplarId, IWeaponData data)
        {
            _exemplarsReplication.Add(exemplarId, new WeaponReplication(data, _castObject));
        }

        protected override void InstantiateData(int exemplarId)
        {
            _exemplarsData.Add(exemplarId, new WeaponData());
        }
    }
}