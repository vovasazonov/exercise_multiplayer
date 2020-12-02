using Replications;
using Serialization;

namespace Models.Characters
{
    public class HealthPointReplication : Replication
    {
        private readonly IHealthPointData _healthPointData;
        private uint _oldPoints;
        private uint _oldMaxPoints;

        public HealthPointReplication(IHealthPointData healthPointData, ICustomCastObject castObject) : base(castObject)
        {
            _healthPointData = healthPointData;

            InstantiateProperty("points", new Property(GetPoints, GetPoints, SetPoints, IsPointsChanged, ResetDiffPoints));
            InstantiateProperty("max_points", new Property(GetMaxPoints, GetPoints, SetMaxPoints, IsMaxPointsChanged, ResetDiffMaxPoints));
        }

        private object GetPoints()
        {
            return _healthPointData.Points;
        }

        private void SetPoints(object obj)
        {
            _healthPointData.Points = _castObject.To<uint>(obj);
        }

        private bool IsPointsChanged()
        {
            return _oldPoints != _healthPointData.Points;
        }

        private void ResetDiffPoints()
        {
            _oldPoints = _healthPointData.Points;
        }

        private object GetMaxPoints()
        {
            return _healthPointData.MaxPoints;
        }

        private void SetMaxPoints(object obj)
        {
            _healthPointData.MaxPoints = _castObject.To<uint>(obj);
        }

        private bool IsMaxPointsChanged()
        {
            return _oldMaxPoints != _healthPointData.MaxPoints;
        }

        private void ResetDiffMaxPoints()
        {
            _oldMaxPoints = _healthPointData.MaxPoints;
        }
    }
}