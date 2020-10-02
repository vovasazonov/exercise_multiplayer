using System;
using Client;

namespace HealthPoints.Models
{
    public class HealthPointModel : IHealthPointModel
    {
        private readonly IClient _client;
        public event Action PointsChanged;
        public uint Points { get; private set;}

        public HealthPointModel(IClient client, uint points)
        {
            _client = client;
            Points = points;
        }
        
        public async void Take(uint points)
        {
            string result = await _client.PostRequest($"take_hp: {points}");
            Points = uint.Parse(result);
            OnPointsChanged();
        }

        private void OnPointsChanged()
        {
            PointsChanged?.Invoke();
        }
    }
}