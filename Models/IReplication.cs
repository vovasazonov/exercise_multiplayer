namespace Models
{
    public interface IReplication
    {
        void SetCustomCast(ICustomCastObject customCastObject);
        void Read(object data);
        object Write(ReplicationType replicationType);
    }

    public enum ReplicationType : byte
    {
        Whole,
        Diff
    }
}