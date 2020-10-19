using System;
using Game.Enemies.Data;

namespace Game.Worlds.Models
{
    public interface IWorldModelClient
    {
        event Action<int> EnemyInstantiated;
        
        void InstantiateEnemy(IEnemyData enemyData);
    }
}