using System;
using System.Collections.Generic;

namespace Models.Characters
{
    public sealed class HealthPointData : Replication, IHealthPointData
    {
        public event EventHandler MaxPointsUpdated;
        public event EventHandler PointsUpdated;
        
        private uint _maxPoints;
        private uint _points;

        public uint MaxPoints
        {
            get => _maxPoints;
            set
            {
                _maxPoints = value;
                _diff[nameof(MaxPoints)] = value;
                OnMaxPointsUpdated();
            }
        }

        public uint Points
        {
            get => _points;
            set
            {
                _points = value;
                _diff[nameof(Points)] = value;
                OnPointsUpdated();
            }
        }

        public override void Read(object data)
        {
            var dataDic = _customCastObject.To<Dictionary<string, object>>(data);
            foreach (var dataId in dataDic.Keys)
            {
                var value = dataDic[dataId];

                switch (dataId)
                {
                    case nameof(MaxPoints):
                        MaxPoints = _customCastObject.To<uint>(value);
                        break;
                    case nameof(Points):
                        Points = _customCastObject.To<uint>(value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        protected override object GetWhole()
        {
            return new Dictionary<string, object>
            {
                {nameof(MaxPoints), MaxPoints},
                {nameof(Points), Points}
            };
        }

        private void OnMaxPointsUpdated()
        {
            MaxPointsUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void OnPointsUpdated()
        {
            PointsUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}