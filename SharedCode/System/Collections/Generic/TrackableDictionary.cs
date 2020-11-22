namespace System.Collections.Generic
{
    public class TrackableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ITrackableDictionary<TKey, TValue>
    {
        public event Action<TKey, TValue> Added;
        public event Action<TKey, TValue> Removed;

        public TrackableDictionary()
        {
        }

        public TrackableDictionary(int capacity) : base(capacity)
        {
        }

        public new void Clear()
        {
            foreach (var key in Keys)
            {
                Remove(key);
            }
        }

        public new void Add(TKey key, TValue value)
        {
            base.Add(key,value);
            OnAdded(key, value);
        }

        public new bool Remove(TKey key)
        {
            bool isRemoved = false;
            
            if (TryGetValue(key, out var value))
            {
                isRemoved = base.Remove(key);
                OnRemoved(key,value);
            }

            return isRemoved;
        }

        public new TValue this[TKey key]
        {
            get => base[key];
            set
            {
                if (ContainsKey(key))
                {
                    Remove(key);
                }
                
                base[key] = value;
                OnAdded(key,value);
            }
        }

        protected virtual void OnAdded(TKey key, TValue value)
        {
            Added?.Invoke(key, value);
        }

        protected virtual void OnRemoved(TKey key, TValue value)
        {
            Removed?.Invoke(key, value);
        }
    }
}