using System;

namespace Replications
{
    public class Property : IReplication
    {
        private readonly Func<object> _getWholeDelegate;
        private readonly Func<object> _getDiffDelegate;
        private readonly Func<bool> _checkDifferDelegate;
        private readonly Action _resetDifferDelegate;
        private readonly Action<object> _setDelegate;
            
        public Property(Func<object> getWholeDelegate,Func<object> getDiffDelegate, Action<object> setDelegate, Func<bool> checkDifferDelegate, Action resetDifferDelegate)
        {
            _getWholeDelegate = getWholeDelegate;
            _getDiffDelegate = getDiffDelegate;
            _setDelegate = setDelegate;
            _checkDifferDelegate = checkDifferDelegate;
            _resetDifferDelegate = resetDifferDelegate;
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
            return _getWholeDelegate.Invoke();
        }

        public object WriteDiff()
        {
            return _getDiffDelegate.Invoke();
        }

        public void Read(object obj)
        {
            _setDelegate.Invoke(obj);
        }
    }
}