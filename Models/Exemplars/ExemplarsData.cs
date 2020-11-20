using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class ExemplarsData<TInterfaceData, TRealizationData> : Replication, IExemplarsData<TInterfaceData> where TRealizationData : Replication, TInterfaceData, new()
    {
        public ITrackableDictionary<int, TInterfaceData> ExemplarDic { get; } = new TrackableDictionary<int, TInterfaceData>();

        public ExemplarsData()
        {
            ExemplarDic.Removing += OnRemoving;
        }
        
        private void OnRemoving(int exemplarId, TInterfaceData exemplar)
        {
            if (!_diff.ContainsKey("remove"))
            {
                _diff.Add("remove", new List<int>());
            }
            
            var exemplarsIds = _diff["remove"] as List<int>;
            exemplarsIds.Add(exemplarId);
        }
        
        public override void Read(Dictionary<string, object> data)
        {
            foreach (var dataId in data.Keys)
            {
                var value = data[dataId];
                switch (dataId)
                {
                    case "update":
                        UpdateData((Dictionary<int, object>) value);
                        break;
                    case "remove":
                        RemoveData((List<int>) value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void UpdateData(Dictionary<int, object> dataDic)
        {
            foreach (var id in dataDic.Keys)
            {
                if (!ExemplarDic.TryGetValue(id, out var exemplar))
                {
                    exemplar = new TRealizationData();
                    ExemplarDic.Add(id, exemplar);
                }

                ((TRealizationData)exemplar).Read((Dictionary<string, object>) dataDic[id]);
            }
        }

        private void RemoveData(List<int> dataIds)
        {
            foreach (var id in dataIds)
            {
                ExemplarDic.Remove(id);
            }
        }

        protected override Dictionary<string, object> GetWhole()
        {
            var dic = new Dictionary<int, object>(ExemplarDic.ToDictionary(k=>k.Key,v=>(object)((TRealizationData)v.Value).Write(ReplicationType.Whole)));
            return new Dictionary<string, object>
            {
                {"update", dic}
            };
        }

        protected override Dictionary<string, object> GetDiff()
        {
            var dic = new Dictionary<int, object>(ExemplarDic.ToDictionary(k=>k.Key,v=>(object)((TRealizationData)v.Value).Write(ReplicationType.Diff)));

            _diff["update"] = dic;

            return base.GetDiff();
        }
    }
}