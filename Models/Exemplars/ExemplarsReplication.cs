using System.Collections.Generic;
using System.Linq;
using Replications;
using Serialization;

namespace Models
{
    public abstract class ExemplarsReplication<TInterfaceData> : Replication
    {
        private const string _update = "update";
        private const string _destroy = "destroy";
        protected readonly ITrackableDictionary<int, TInterfaceData> _exemplarsData;
        protected readonly Dictionary<int, Replication> _exemplarsReplication = new Dictionary<int, Replication>();

        protected ExemplarsReplication(ITrackableDictionary<int, TInterfaceData> exemplarsData, ICustomCastObject castObject) : base(castObject)
        {
            _exemplarsData = exemplarsData;

            _exemplarsData.Added += OnDataAdded;
            _exemplarsData.Removed += OnDataRemoved;

            _getterDic.Add(_update, () => _exemplarsReplication.ToDictionary(key => key.Key, value => value.Value.WriteWhole()));
            _setterDic.Add(_update, UpdateData);
            _setterDic.Add(_destroy, DestroyData);

            InstantiateReplications();
        }

        private void InstantiateReplications()
        {
            foreach (var id in _exemplarsData.Keys)
            {
                InstantiateReplication(id, _exemplarsData[id]);
            }
        }

        protected abstract void InstantiateReplication(int exemplarId, TInterfaceData data);
        protected abstract void InstantiateData(int exemplarId);
        
        private void OnDataAdded(int exemplarId, TInterfaceData data)
        {
            InstantiateReplication(exemplarId, data);
        }

        private void OnDataRemoved(int exemplarId, TInterfaceData exemplar)
        {
            if (!_diffDic.ContainsKey("destroy"))
            {
                _diffDic.Add("destroy", new List<int>());
            }

            var exemplarsIds = _diffDic["destroy"] as List<int>;
            exemplarsIds?.Add(exemplarId);

            _exemplarsReplication.Remove(exemplarId);
        }

        private void UpdateData(object obj)
        {
            var dataDic = _castObject.To<Dictionary<int, object>>(obj);

            foreach (var id in dataDic.Keys)
            {
                if (!_exemplarsData.TryGetValue(id, out var exemplar))
                {
                    InstantiateData(id);
                }

                _exemplarsReplication[id].Read(dataDic[id]);
            }
        }

        private void DestroyData(object obj)
        {
            var dataIds = _castObject.To<List<int>>(obj);
            foreach (var id in dataIds)
            {
                _exemplarsData.Remove(id);
            }
        }

        public override object WriteDiff()
        {
            var dic = new Dictionary<int, object>(_exemplarsReplication.ToDictionary(k => k.Key, v => v.Value.WriteDiff()));
            _diffDic["update"] = dic;

            return base.WriteDiff();
        }
    }
}