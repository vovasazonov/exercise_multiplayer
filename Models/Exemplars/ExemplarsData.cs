using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public sealed class ExemplarsData<TInterfaceData, TRealizationData> : Replication, IExemplarsData<TInterfaceData> where TRealizationData : Replication, TInterfaceData, new()
    {
        public event EventHandler Updated;
        
        public ITrackableDictionary<int, TInterfaceData> ExemplarDic { get; } = new TrackableDictionary<int, TInterfaceData>();

        public ExemplarsData()
        {
            ExemplarDic.Removed += OnRemoved;
            ExemplarDic.Added += OnAdded;
        }

        private void OnAdded(int exemplarId, TInterfaceData exemplar)
        {
            SetCustomCastToExemplar(exemplar);
            OnUpdated();
        }

        private void OnRemoved(int exemplarId, TInterfaceData exemplar)
        {
            if (!_diff.ContainsKey("remove"))
            {
                _diff.Add("remove", new List<int>());
            }
            
            var exemplarsIds = _diff["remove"] as List<int>;
            exemplarsIds.Add(exemplarId);
            
            OnUpdated();
        }

        public override void SetCustomCast(ICustomCastObject customCastObject)
        {
            base.SetCustomCast(customCastObject);

            foreach (var interfaceData in ExemplarDic.Values)
            {
                SetCustomCastToExemplar(interfaceData);
            }
        }

        private void SetCustomCastToExemplar(TInterfaceData exemplar)
        {
            ((TRealizationData)exemplar).SetCustomCast(_customCastObject);
        }

        public override void Read(object data)
        {
            var dataDic = _customCastObject.To<Dictionary<string, object>>(data);
            foreach (var dataId in dataDic.Keys)
            {
                var value = dataDic[dataId];
                switch (dataId)
                {
                    case "update":
                        UpdateData(value);
                        break;
                    case "remove":
                        RemoveData(value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void UpdateData(object data)
        {
            var dataDic = _customCastObject.To<Dictionary<int, object>>(data);
            
            foreach (var id in dataDic.Keys)
            {
                if (!ExemplarDic.TryGetValue(id, out var exemplar))
                {
                    exemplar = new TRealizationData();
                    ExemplarDic.Add(id, exemplar);
                }

                ((TRealizationData)exemplar).Read(dataDic[id]);
            }
        }

        private void RemoveData(object data)
        {
            var dataIds = _customCastObject.To<List<int>>(data);
            foreach (var id in dataIds)
            {
                ExemplarDic.Remove(id);
            }
        }

        protected override object GetWhole()
        {
            var dic = new Dictionary<int, object>(ExemplarDic.ToDictionary(k=>k.Key,v=>((TRealizationData)v.Value).Write(ReplicationType.Whole)));
            return new Dictionary<string, object>
            {
                {"update", dic}
            };
        }

        protected override object GetDiff()
        {
            var dic = new Dictionary<int, object>(ExemplarDic.ToDictionary(k=>k.Key,v=>((TRealizationData)v.Value).Write(ReplicationType.Diff)));

            _diff["update"] = dic;

            return base.GetDiff();
        }

        private void OnUpdated()
        {
            Updated?.Invoke(this, EventArgs.Empty);
        }
    }
}