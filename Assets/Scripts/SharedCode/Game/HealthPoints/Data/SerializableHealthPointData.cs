using System.Runtime.Serialization;

namespace Game.HealthPoints.Data
{
    [DataContract]
    public class SerializableHealthPointData : IHealthPointData
    {
        [DataMember(Name = "points")] public uint Points { get; set; }

        public SerializableHealthPointData()
        {
        }

        public SerializableHealthPointData(IHealthPointData data)
        {
            Points = data.Points;
        }
    }
}