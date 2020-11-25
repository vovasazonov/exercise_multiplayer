using System;
using System.Collections.Generic;

namespace Replications
{
    public abstract class Replication : IReplication
    {
        protected readonly ICustomCastObject _castObject;
        protected readonly Dictionary<string, object> _diffDic = new Dictionary<string, object>();
        protected readonly Dictionary<string, Func<object>> _getterDic = new Dictionary<string, Func<object>>();
        protected readonly Dictionary<string, Action<object>> _setterDic = new Dictionary<string, Action<object>>();

        protected Replication(ICustomCastObject castObject)
        {
            _castObject = castObject;
        }

        public object WriteWhole()
        {
            var dic = new Dictionary<string, object>(_getterDic.Count);

            foreach (var key in _getterDic.Keys)
            {
                dic.Add(key, _getterDic[key].Invoke());
            }

            return dic;
        }

        public object WriteDiff()
        {
            var replicatedData = new Dictionary<string, object>(_diffDic);
            _diffDic.Clear();
            return replicatedData;
        }
        
        public void Read(object obj)
        {
            Dictionary<string, object> dataDic = (Dictionary<string, object>) obj;

            foreach (var key in dataDic.Keys)
            {
                _setterDic[key].Invoke(dataDic[key]);
            }
        }
    }
}