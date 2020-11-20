using System;
using System.Collections.Generic;

namespace Models
{
    public abstract class Replication : IReplication
    {
        protected readonly Dictionary<string, object> _diff = new Dictionary<string, object>();

        public abstract void Read(Dictionary<string, object> data);

        public virtual Dictionary<string, object> Write(ReplicationType replicationType)
        {
            Dictionary<string, object> data;

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

        protected abstract Dictionary<string, object> GetWhole();
        
        protected virtual Dictionary<string, object> GetDiff()
        {
            var dataDic = new Dictionary<string, object>(_diff);
            _diff.Clear();

            return dataDic.Count > 0 ? dataDic : null;
        }
    }
}