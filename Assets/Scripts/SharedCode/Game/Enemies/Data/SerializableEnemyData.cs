using System.Runtime.Serialization;
using Game.HealthPoints.Data;

namespace Game.Enemies.Data
{
    [DataContract]
    public class SerializableEnemyData : IEnemyData
    {
        [DataMember(Name = "health_points")]
        private SerializableHealthPointData SerializableHealthPoint
        {
            get => HealthPoint != null ? new SerializableHealthPointData(HealthPoint) : null;
            set => HealthPoint = value;
        }

        [DataMember(Name = "id")] public int Id { get; set; }
        public IHealthPointData HealthPoint { get; set; }

        public SerializableEnemyData()
        {
        }

        public SerializableEnemyData(IEnemyData data)
        {
            Id = data.Id;
            HealthPoint = data.HealthPoint;
        }
    }
}