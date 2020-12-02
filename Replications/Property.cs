using System;

namespace Replications
{
    public class Property : IReplication
    {
        private readonly Func<object> _getDelegate;
        private readonly Func<bool> _checkDifferDelegate;
        private readonly Action _resetDifferDelegate;
        private readonly Action<object> _setDelegate;
            
        public Property(Func<object> getDelegate, Action<object> setDelegate, Func<bool> checkDifferDelegate, Action resetDifferDelegate)
        {
            _getDelegate = getDelegate;
            _checkDifferDelegate = checkDifferDelegate;
            _resetDifferDelegate = resetDifferDelegate;
            _setDelegate = setDelegate;
        }

        public bool ContainsDiff()
        {
            return _checkDifferDelegate.Invoke();
        }

        public void ResetDiff()
        {
            _resetDifferDelegate.Invoke();
        }

        public object WriteWhole()
        {
            return _getDelegate.Invoke();
        }

        public object WriteDiff()
        {
            return WriteWhole();
        }

        public void Read(object obj)
        {
            _setDelegate.Invoke(obj);
        }
    }
}