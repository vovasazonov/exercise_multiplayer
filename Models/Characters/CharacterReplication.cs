using System;
using Replications;
using Serialization;

namespace Models.Characters
{
    public class CharacterReplication : Replication
    {
        private readonly ICharacterData _characterData;
        private readonly HealthPointReplication _healthPointReplication;

        public CharacterReplication(ICharacterData characterData, ICustomCastObject castObject) : base(castObject)
        {
            _characterData = characterData;
            _healthPointReplication = new HealthPointReplication(_characterData.HealthPointData, castObject);

            _characterData.HoldWeaponUpdated += OnHoldWeaponUpdated;

            _getterDic.Add(nameof(_characterData.Id), () => _characterData.Id);
            _setterDic.Add(nameof(_characterData.Id), obj => _characterData.Id = castObject.To<string>(obj));
            _getterDic.Add(nameof(_characterData.HoldWeaponExemplarId), () => _characterData.HoldWeaponExemplarId);
            _setterDic.Add(nameof(_characterData.HoldWeaponExemplarId), obj => _characterData.HoldWeaponExemplarId = castObject.To<int>(obj));
            _getterDic.Add(nameof(_characterData.HealthPointData), () => _healthPointReplication.WriteWhole());
            _setterDic.Add(nameof(_characterData.HealthPointData), obj => _healthPointReplication.Read(obj));
        }

        private void OnHoldWeaponUpdated(object sender, EventArgs e) => _diffDic[nameof(_characterData.HoldWeaponExemplarId)] = _characterData.HoldWeaponExemplarId;
        public override object WriteDiff()
        {
            _diffDic[nameof(_characterData.HealthPointData)] = _healthPointReplication.WriteDiff();
            return base.WriteDiff();
        }
    }
}