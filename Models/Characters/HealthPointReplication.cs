using System;
using Replications;
using Serialization;

namespace Models.Characters
{
    public class HealthPointReplication : Replication
    {
        private readonly IHealthPointData _healthPointData;

        public HealthPointReplication(IHealthPointData healthPointData, ICustomCastObject castObject) : base(castObject)
        {
            _healthPointData = healthPointData;
            
            _healthPointData.PointsUpdated += OnPointsUpdated;
            _healthPointData.MaxPointsUpdated += OnMaxPointsUpdated;

            _getterDic.Add(nameof(_healthPointData.Points), () => _healthPointData.Points);
            _setterDic.Add(nameof(_healthPointData.Points), obj => _healthPointData.Points = _castObject.To<uint>(obj));
            _getterDic.Add(nameof(_healthPointData.MaxPoints), () => _healthPointData.MaxPoints);
            _setterDic.Add(nameof(_healthPointData.MaxPoints), obj => _healthPointData.MaxPoints = _castObject.To<uint>(obj));
        }

        private void OnPointsUpdated(object sender, EventArgs e) => _diffDic[nameof(_healthPointData.Points)] = _healthPointData.Points;
        private void OnMaxPointsUpdated(object sender, EventArgs e) => _diffDic[nameof(_healthPointData.MaxPoints)] = _healthPointData.MaxPoints;
    }
}