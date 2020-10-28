namespace System.Collections.Generic
{
    public class TrackableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ITrackableDictionary<TKey, TValue>
    {
        public event Action<TKey, TValue> Adding;
        public event Action<TKey, TValue> Removing;

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
                OnRemoving(key,base[key]);
            }
            
            base.Clear();
        }

        public new void Add(TKey key, TValue value)
        {
            OnAdding(key, value);
            base.Add(key,value);
        }

        public new bool Remove(TKey key)
        {
            if (TryGetValue(key, out var value))
            {
                OnRemoving(key,value);
            }

            return base.Remove(key);
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
                
                OnAdding(key,value);
                base[key] = value;
            }
        }

        protected virtual void OnAdding(TKey key, TValue value)
        {
            Adding?.Invoke(key, value);
        }

        protected virtual void OnRemoving(TKey key, TValue value)
        {
            Removing?.Invoke(key, value);
        }
    }
}