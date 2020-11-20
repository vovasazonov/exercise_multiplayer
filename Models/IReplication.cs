using System.Collections.Generic;

namespace Models
{
    public interface IReplication
    {
        void Read(Dictionary<string, object> data);
        Dictionary<string, object> Write(ReplicationType replicationType);
    }

    public enum ReplicationType : byte
    {
        Whole,
        Diff
    }
}