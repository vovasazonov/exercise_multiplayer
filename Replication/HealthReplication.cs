using System;

namespace Replications
{
    public sealed class HealthReplication : Replication
    {
        private readonly IHealthData _healthData;

        public HealthReplication(ICustomCastObject castObject, IHealthData healthData) : base(castObject)
        {
            _healthData = healthData;
            _healthData.CurrentPointsUpdated += OnCurrentPointsUpdated;

            _getterDic.Add(nameof(_healthData.CurrentPoints), () => _healthData.CurrentPoints);
            _setterDic.Add(nameof(_healthData.CurrentPoints), obj => _healthData.CurrentPoints = _castObject.To<int>(obj));
        }

        private void OnCurrentPointsUpdated(object? sender, EventArgs e) => _diffDic[nameof(_healthData.CurrentPoints)] = _healthData.CurrentPoints;
    }
}