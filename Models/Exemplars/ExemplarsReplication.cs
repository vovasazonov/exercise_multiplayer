using System.Collections.Generic;
using System.Linq;
using Replications;
using Serialization;

namespace Models
{
    public abstract class ExemplarsReplication<TInterfaceData> : Replication
    {
        protected readonly ITrackableDictionary<int, TInterfaceData> _exemplarsData;
        protected readonly Dictionary<int, Replication> _exemplarsReplication = new Dictionary<int, Replication>();
        private readonly List<int> _removedDataList = new List<int>();

        protected ExemplarsReplication(ITrackableDictionary<int, TInterfaceData> exemplarsData, ICustomCastObject castObject) : base(castObject)
        {
            _exemplarsData = exemplarsData;

            AddListener();

            InstantiateProperty("data", new Property(GetData, GetDiffData, SetData, IsDataDiff, ResetDataDiff));
            InstantiateProperty("removeData", new Property(GetRemoveData, GetRemoveData, SetRemoveData, IsRemoveDataDiff, ResetRemoveDataDiff));

            InstantiateReplications();
        }

        private object GetDiffData()
        {
            return new Dictionary<int, object>(_exemplarsReplication.ToDictionary(k => k.Key, v => v.Value.WriteDiff()));
        }

        private object GetData()
        {
            return new Dictionary<int, object>(_exemplarsReplication.ToDictionary(k => k.Key, v => v.Value.WriteWhole()));
        }

        private void SetData(object obj)
        {
            var dataDic = _castObject.To<Dictionary<int, object>>(obj);

            foreach (var id in dataDic.Keys)
            {
                if (!_exemplarsData.ContainsKey(id))
                {
                    InstantiateData(id);
                }

                _exemplarsReplication[id].Read(dataDic[id]);
            }
        }

        private bool IsDataDiff()
        {
            return _exemplarsReplication.Values.Any(r => r.ContainsDiff());
        }

        private void ResetDataDiff()
        {
            foreach (var replicationValue in _exemplarsReplication.Values)
            {
                replicationValue.ResetDiff();
            }
        }

        private object GetRemoveData()
        {
            return new List<int>(_removedDataList);
        }

        private void SetRemoveData(object obj)
        {
            var dataIds = _castObject.To<List<int>>(obj);
            foreach (var id in dataIds)
            {
                _exemplarsData.Remove(id);
            }
        }

        private bool IsRemoveDataDiff()
        {
            return _removedDataList.Count > 0;
        }

        private void ResetRemoveDataDiff()
        {
            _removedDataList.Clear();
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
            _removedDataList.Add(exemplarId);
            _exemplarsReplication.Remove(exemplarId);
        }

        private void AddListener()
        {
            _exemplarsData.Added += OnDataAdded;
            _exemplarsData.Removed += OnDataRemoved;
        }

        private void RemoveListener()
        {
            _exemplarsData.Added -= OnDataAdded;
            _exemplarsData.Removed -= OnDataRemoved;
        }
    }
}