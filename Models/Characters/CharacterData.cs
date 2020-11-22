using System;
using System.Collections.Generic;

namespace Models.Characters
{
    public sealed class CharacterData : Replication, ICharacterData
    {
        public event EventHandler HoldWeaponUpdated;

        private string _id;
        private int _holdWeaponExemplarId;
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

        public int HoldWeaponExemplarId
        {
            get => _holdWeaponExemplarId;
            set
            {
                _holdWeaponExemplarId = value;
                _diff[nameof(HoldWeaponExemplarId)] = value;
                OnHoldWeaponUpdated();
            }
        }

        public IHealthPointData HealthPointData => _healthPointData;

        public override void SetCustomCast(ICustomCastObject customCastObject)
        {
            base.SetCustomCast(customCastObject);
            _healthPointData.SetCustomCast(customCastObject);
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
                        Id = _customCastObject.To<string>(value);
                        break;
                    case nameof(HoldWeaponExemplarId):
                        HoldWeaponExemplarId = _customCastObject.To<int>(value);
                        break;
                    case nameof(HealthPointData):
                        _healthPointData.Read(_customCastObject.To<Dictionary<string, object>>(value));
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
                {nameof(HoldWeaponExemplarId), _holdWeaponExemplarId},
                {nameof(HealthPointData), _healthPointData.Write(ReplicationType.Whole)}
            };
        }

        protected override object GetDiff()
        {
            _diff.Add(nameof(HealthPointData), _healthPointData.Write(ReplicationType.Diff));
            
            return base.GetDiff();
        }
        
        private void OnHoldWeaponUpdated()
        {
            HoldWeaponUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}