namespace Replications
{
    public interface IReplication
    {
        object WriteWhole();
        object WriteDiff();
        void Read(object obj);
    }
}