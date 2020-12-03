using Replications;
using Serialization;

namespace Models.Weapons
{
    public class WeaponReplication : Replication
    {
        public WeaponReplication(IWeaponData weaponData, ICustomCastObject castObject) : base(castObject)
        {
            InstantiateProperty("id", new PrimitiveProperty<string>(() => weaponData.Id,obj => weaponData.Id = _castObject.To<string>(obj)));
            InstantiateProperty("damage", new PrimitiveProperty<uint>(() => weaponData.Damage,obj => weaponData.Damage = _castObject.To<uint>(obj)));
        }
    }
}