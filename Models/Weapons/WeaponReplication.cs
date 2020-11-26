using System;
using Replications;
using Serialization;

namespace Models.Weapons
{
    public class WeaponReplication : Replication
    {
        private readonly IWeaponData _weaponData;

        public WeaponReplication(IWeaponData weaponData, ICustomCastObject castObject) : base(castObject)
        {
            _weaponData = weaponData;

            weaponData.DamageUpdated += OnDamageUpdated;

            _getterDic.Add(nameof(_weaponData.Id), () => _weaponData.Id);
            _setterDic.Add(nameof(_weaponData.Id), obj => _weaponData.Id = _castObject.To<string>(obj));
            _getterDic.Add(nameof(_weaponData.Damage), () => _weaponData.Damage);
            _setterDic.Add(nameof(_weaponData.Damage), obj => _weaponData.Damage = _castObject.To<uint>(obj));
        }

        private void OnDamageUpdated(object sender, EventArgs e) => _diffDic[nameof(_weaponData.Damage)] = _weaponData.Damage;
    }
}