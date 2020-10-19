using System;
using System.Collections.Generic;
using Game.Enemies.Data;
using Game.Enemies.Models;

namespace Game.Worlds.Models
{
    public class WorldModel : IWorldModelClient
    {
        private readonly Dictionary<int, IEnemyModel> _enemies;
        public event Action<int> EnemyInstantiated;

        public WorldModel(Dictionary<int, IEnemyModel> enemies)
        {
            _enemies = enemies;
        }
        
        public void InstantiateEnemy(IEnemyData enemyData)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnEnemyInstantiated(int idEnemy)
        {
            EnemyInstantiated?.Invoke(idEnemy);
        }
    }
}