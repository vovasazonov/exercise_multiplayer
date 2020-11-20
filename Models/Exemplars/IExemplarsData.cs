using System.Collections.Generic;

namespace Models
{
    public interface IExemplarsData<TInterfaceData>
    {
        ITrackableDictionary<int, TInterfaceData> ExemplarDic { get; }
    }
}