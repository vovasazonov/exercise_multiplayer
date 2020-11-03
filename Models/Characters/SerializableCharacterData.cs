using System;
using System.Runtime.Serialization;

namespace Models.Characters
{
    [Serializable]
    [DataContract]
    public class SerializableCharacterData : ICharacterData
    {
        [DataMember] private SerializableHealthPointData _serializableHealthPointDataHelper;

        [DataMember] public string Id { get; set; }
        [DataMember] public string HoldWeaponId { get; set; }
        
        public IHealthPointData HealthPointData
        {
            get => _serializableHealthPointDataHelper;
            set => _serializableHealthPointDataHelper = new SerializableHealthPointData(value);
        }
        
        public SerializableCharacterData(){}

        public SerializableCharacterData(ICharacterData data)
        {
            Id = data.Id;
            HoldWeaponId = data.HoldWeaponId;
            _serializableHealthPointDataHelper = new SerializableHealthPointData(data.HealthPointData);
        }
    }
}