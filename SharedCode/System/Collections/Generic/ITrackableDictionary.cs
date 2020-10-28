namespace System.Collections.Generic
{
    public interface ITrackableDictionary<TKey,TValue> : IDictionary<TKey,TValue>
    {
        event Action<TKey, TValue> Adding;
        event Action<TKey, TValue> Removing;
    }
}