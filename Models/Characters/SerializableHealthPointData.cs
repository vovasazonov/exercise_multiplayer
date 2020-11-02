using System;
using System.Runtime.Serialization;

namespace Models.Characters
{
    [Serializable]
    [DataContract]
    public struct SerializableHealthPointData : IHealthPointData
    {
        [DataMember] public uint MaxPoints { get; set; }
        [DataMember] public uint Points { get; set; }
        
        public SerializableHealthPointData(IHealthPointData data)
        {
            MaxPoints = data.MaxPoints;
            Points = data.Points;
        }
    }
}