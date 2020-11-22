using System;
using System.Collections.Generic;

namespace Models
{
    public interface IExemplarsData<TInterfaceData>
    {
        event EventHandler Updated;
        ITrackableDictionary<int, TInterfaceData> ExemplarDic { get; }
    }
}