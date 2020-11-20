using System;
using System.Collections.Generic;

namespace Models.Weapons
{
    public class WeaponData : Replication, IWeaponData
    {
        public event EventHandler DamageUpdated;
        
        private string _id;
        private uint _damage;

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                _diff[nameof(Id)] = value;
            }
        }

        public uint Damage
        {
            get => _damage;
            set
            {
                _damage = value;
                _diff[nameof(Damage)] = value;
                OnDamageUpdated();
            }
        }

        public override void Read(Dictionary<string, object> data)
        {
            foreach (var dataId in data.Keys)
            {
                var value = data[dataId];

                switch (dataId)
                {
                    case nameof(Id):
                        Id = (string) value;
                        break;
                    case nameof(Damage):
                        Damage = (uint) value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        protected override Dictionary<string, object> GetWhole()
        {
            return new Dictionary<string, object>
            {
                {nameof(Id), Id},
                {nameof(Damage), Damage}
            };
        }

        private void OnDamageUpdated()
        {
            DamageUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}