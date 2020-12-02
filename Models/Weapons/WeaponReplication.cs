using Replications;
using Serialization;

namespace Models.Weapons
{
    public class WeaponReplication : Replication
    {
        private readonly IWeaponData _weaponData;
        private string _oldId;
        private uint _oldDamage;

        public WeaponReplication(IWeaponData weaponData, ICustomCastObject castObject) : base(castObject)
        {
            _weaponData = weaponData;

            InstantiateProperty("id", new Property(GetId, GetId, SetId, ContainsDiffId, ResetDiffId));
            InstantiateProperty("damage", new Property(GetDamage, GetDamage, SetDamage, ContainsDiffDamage, ResetDiffDamage));
        }

        private object GetId()
        {
            return _weaponData.Id;
        }

        private void SetId(object obj)
        {
            _weaponData.Id = _castObject.To<string>(obj);
        }

        private bool ContainsDiffId()
        {
            return _weaponData.Id != _oldId;
        }

        private void ResetDiffId()
        {
            _oldId = _weaponData.Id;
        }

        private object GetDamage()
        {
            return _weaponData.Damage;
        }

        private void SetDamage(object obj)
        {
            _weaponData.Damage = _castObject.To<uint>(obj);
        }

        private bool ContainsDiffDamage()
        {
            return _weaponData.Damage != _oldDamage;
        }

        private void ResetDiffDamage()
        {
            _oldDamage = _weaponData.Damage;
        }
    }
}