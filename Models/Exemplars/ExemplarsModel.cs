using System.Collections.Generic;

namespace Models
{
    public abstract class ExemplarsModel<TModel, TData> : IExemplarsModel<TModel>
    {
        private readonly ITrackableDictionary<int, TData> _exemplarsData;

        public ITrackableDictionary<int, TModel> ExemplarModelDic { get; } = new TrackableDictionary<int, TModel>();

        public ExemplarsModel(ITrackableDictionary<int, TData> exemplarsData)
        {
            _exemplarsData = exemplarsData;

            _exemplarsData.Added += OnAdded;
            _exemplarsData.Removed += OnRemoved;

            Instantiate();
        }

        private void Instantiate()
        {
            foreach (var id in _exemplarsData.Keys)
            {
                AddModel(id, _exemplarsData[id]);
            }
        }

        private void OnAdded(int id, TData data)
        {
            AddModel(id, data);
        }

        private void OnRemoved(int id, TData data)
        {
            RemoveModel(id);
        }

        protected abstract void AddModel(int id, TData data);

        private void RemoveModel(int id)
        {
            ExemplarModelDic.Remove(id);
        }
    }
}