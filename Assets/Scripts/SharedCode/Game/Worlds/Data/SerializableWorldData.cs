using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Game.Enemies.Data;

namespace Game.Worlds.Data
{
    [DataContract]
    public class SerializableWorldData : IWorldData
    {
        [DataMember(Name = "enemies")]
        private List<SerializableEnemyData> SerializableEnemies
        {
            get => Enemies?.ConvertAll(t => new SerializableEnemyData(t));
            set => _helperSerializableEnemies = value;
        }

        private List<SerializableEnemyData> _helperSerializableEnemies;
        
        public List<IEnemyData> Enemies { get; set; }
        
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            SetDeserializationToData();
        }

        private void SetDeserializationToData()
        {
            Enemies = _helperSerializableEnemies.Select(r => (IEnemyData) r).ToList();
        }
    }
}