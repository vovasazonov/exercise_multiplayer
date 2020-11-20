using System.Collections.Generic;

namespace Models
{
    public interface IExemplarsModel<TModel>
    {
        ITrackableDictionary<int, TModel> ExemplarModelDic { get; }
    }
}