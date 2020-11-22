namespace System.Collections.Generic
{
    public interface ITrackableDictionary<TKey,TValue> : IDictionary<TKey,TValue>
    {
        event Action<TKey, TValue> Added;
        event Action<TKey, TValue> Removed;
    }
}