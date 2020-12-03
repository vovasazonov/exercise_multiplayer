namespace Replications
{
    public class ReplicationProperty : IReplication
    {
        private readonly IReplication _replication;

        public ReplicationProperty(IReplication replication)
        {
            _replication = replication;
        }
        
        public bool ContainsDiff()
        {
            return _replication.ContainsDiff();
        }

        public void ResetDiff()
        {
            _replication.ResetDiff();
        }

        public object WriteDiff()
        {
            return _replication.WriteDiff();
        }

        public object WriteWhole()
        {
            return _replication.WriteWhole();
        }

        public void Read(object obj)
        {
            _replication.WriteWhole();
        }
    }
}