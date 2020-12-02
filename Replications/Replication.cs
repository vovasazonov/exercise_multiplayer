using System.Collections.Generic;
using System.Linq;
using Serialization;

namespace Replications
{
    public abstract class Replication : IReplication
    {
        protected readonly ICustomCastObject _castObject;
        private readonly Dictionary<string, IReplication> _properties = new Dictionary<string, IReplication>();

        protected Replication(ICustomCastObject castObject)
        {
            _castObject = castObject;
        }

        protected void InstantiateProperty(string id, IReplication property)
        {
            _properties[id] = property;
        }

        public bool ContainsDiff()
        {
            return _properties.Any(p => p.Value.ContainsDiff());
        }

        public void ResetDiff()
        {
            foreach (var property in _properties.Values)
            {
                property.ResetDiff();
            }
        }

        public object WriteWhole()
        {
            return _properties.ToDictionary(k => k.Key, v => v.Value.WriteWhole());
        }

        public object WriteDiff()
        {
            return _properties.ToDictionary(k => k.Key, v => v.Value.WriteDiff());
        }

        public void Read(object obj)
        {
            Dictionary<string, object> dataDic = _castObject.To<Dictionary<string, object>>(obj);

            foreach (var key in dataDic.Keys)
            {
                _properties[key].Read(dataDic[key]);
            }
        }
    }
}