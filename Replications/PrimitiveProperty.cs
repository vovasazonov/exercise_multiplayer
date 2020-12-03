using System;

namespace Replications
{
    public class PrimitiveProperty<T> : IReplication
    {
        private T _oldValue;
        private readonly Func<T> _getDelegate;
        private readonly Action<object> _setDelegate;

        public PrimitiveProperty(Func<T> getDelegate, Action<object> setDelegate)
        {
            _getDelegate = getDelegate;
            _setDelegate = setDelegate;
            ResetDiff();
        }

        public bool ContainsDiff()
        {
            return !_oldValue.Equals(_getDelegate.Invoke());
        }

        public void ResetDiff()
        {
            _oldValue = _getDelegate.Invoke();
        }

        public object WriteDiff()
        {
            return _getDelegate.Invoke();
        }

        public object WriteWhole()
        {
            return _getDelegate.Invoke();
        }

        public void Read(object obj)
        {
            _setDelegate.Invoke(obj);
        }
    }
}