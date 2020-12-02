namespace Replications
{
    public interface IReplication
    {
        bool ContainsDiff();
        void ResetDiff();
        object WriteDiff();
        object WriteWhole();
        void Read(object obj);
    }
}