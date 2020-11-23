﻿using System.Collections.Generic;

namespace Models
{
    public abstract class ExemplarsModel<TModel, TData> : IExemplarsModel<TModel>
    {
        private readonly IExemplarsData<TData> _exemplarsData;
        
        public ITrackableDictionary<int, TModel> ExemplarModelDic { get; } = new TrackableDictionary<int, TModel>();

        public ExemplarsModel(IExemplarsData<TData> exemplarsData)
        {
            _exemplarsData = exemplarsData;

            _exemplarsData.ExemplarDic.Added += OnAdded;
            _exemplarsData.ExemplarDic.Removed += OnRemoved;

            Instantiate();
        }

        private void Instantiate()
        {
            foreach (var id in _exemplarsData.ExemplarDic.Keys)
            {
                AddModel(id, _exemplarsData.ExemplarDic[id]);
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