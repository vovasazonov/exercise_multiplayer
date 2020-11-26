using System;
using System.Collections.Generic;

namespace Models.Weapons
{
    public sealed class WeaponData : IWeaponData
    {
        public event EventHandler DamageUpdated;

        private uint _damage;

        public string Id { get; set; }

        public uint Damage
        {
            get => _damage;
            set
            {
                _damage = value;
                OnDamageUpdated();
            }
        }

        private void OnDamageUpdated()
        {
            DamageUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}