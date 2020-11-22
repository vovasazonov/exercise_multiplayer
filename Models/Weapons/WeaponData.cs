using System;
using System.Collections.Generic;

namespace Models.Weapons
{
    public sealed class WeaponData : Replication, IWeaponData
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

        public override void Read(object data)
        {
            var dataDic = _customCastObject.To<Dictionary<string, object>>(data);
            
            foreach (var dataId in dataDic.Keys)
            {
                var value = dataDic[dataId];

                switch (dataId)
                {
                    case nameof(Id):
                        Id = _customCastObject.To<string>(value);;
                        break;
                    case nameof(Damage):
                        Damage = _customCastObject.To<uint>(value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        protected override object GetWhole()
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