using System;

namespace Models.Weapons
{
    public sealed class WeaponData : IWeaponData
    {
        public event Action DamageUpdated;

        private uint _damage;

        public string Id { get; set; }

        public uint Damage
        {
            get => _damage;
            set
            {
                _damage = value;
                CallDamageUpdated();
            }
        }

        private void CallDamageUpdated()
        {
            DamageUpdated?.Invoke();
        }
    }
}