using System;
using System.Collections.Generic;

namespace Models
{
    public abstract class Replication : IReplication
    {
        protected readonly Dictionary<string, object> _diff = new Dictionary<string, object>();
        protected ICustomCastObject _customCastObject;

        public virtual void SetCustomCast(ICustomCastObject customCastObject)
        {
            _customCastObject = customCastObject;
        }

        public abstract void Read(object data);

        public virtual object Write(ReplicationType replicationType)
        {
            object data;

            switch (replicationType)
            {
                case ReplicationType.Whole:
                    data = GetWhole();
                    break;
                case ReplicationType.Diff:
                    data = GetDiff();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(replicationType), replicationType, null);
            }

            return data;
        }

        protected abstract object GetWhole();
        
        protected virtual object GetDiff()
        {
            var dataDic = new Dictionary<string, object>(_diff);
            _diff.Clear();

            return dataDic;
        }
    }
}