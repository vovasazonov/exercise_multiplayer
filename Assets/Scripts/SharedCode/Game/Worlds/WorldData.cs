using System;

namespace Game.Worlds
{
    public class WorldData : IWorldData
    {
        public event Action DataChanged;

        private void OnDataChanged()
        {
            DataChanged?.Invoke();
        }
    }
}
