using System;
using System.Collections.Generic;

namespace Models.Characters
{
    public class CharacterData : Replication, ICharacterData
    {
        public event EventHandler HoldWeaponUpdated;

        private string _id;
        private string _holdWeaponId;
        private readonly HealthPointData _healthPointData = new HealthPointData();

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                _diff[nameof(Id)] = value;
            }
        }

        public string HoldWeaponId
        {
            get => _holdWeaponId;
            set
            {
                _holdWeaponId = value;
                _diff[nameof(HoldWeaponId)] = value;
                OnHoldWeaponUpdated();
            }
        }

        public IHealthPointData HealthPointData => _healthPointData;

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
                    case nameof(HoldWeaponId):
                        HoldWeaponId = (string) value;
                        break;
                    case nameof(HealthPointData):
                        _healthPointData.Read((Dictionary<string, object>) value);
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
                {nameof(HoldWeaponId), HoldWeaponId},
                {nameof(HealthPointData), _healthPointData.Write(ReplicationType.Whole)}
            };
        }

        protected override Dictionary<string, object> GetDiff()
        {
            var dataDic = base.GetDiff();
            dataDic.Add(nameof(HealthPointData), _healthPointData.Write(ReplicationType.Diff));
            return dataDic;
        }
        
        private void OnHoldWeaponUpdated()
        {
            HoldWeaponUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}