namespace System.Collections.Generic
{
    public static class TrackableDictionaryExtensions
    {
        public static int Add<TValue>(this ITrackableDictionary<int, TValue> dic, TValue value)
        {
            int newId = 0;

            while (dic.ContainsKey(newId))
            {
                newId++;
            }

            dic.Add(newId, value);
            
            return newId;
        }
    }
}