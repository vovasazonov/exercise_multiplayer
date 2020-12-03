using Replications;
using Serialization;

namespace Models.Characters
{
    public class HealthPointReplication : Replication
    {
        public HealthPointReplication(IHealthPointData healthPointData, ICustomCastObject castObject) : base(castObject)
        {
            InstantiateProperty("points", new PrimitiveProperty<uint>(() => healthPointData.Points, obj => healthPointData.Points = _castObject.To<uint>(obj)));
            InstantiateProperty("max_points", new PrimitiveProperty<uint>(() => healthPointData.MaxPoints, obj => healthPointData.MaxPoints = _castObject.To<uint>(obj)));
        }
    }
}