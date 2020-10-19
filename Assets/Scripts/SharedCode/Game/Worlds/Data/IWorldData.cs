using System.Collections.Generic;
using Game.Enemies.Data;

namespace Game.Worlds.Data
{
    public interface IWorldData
    {
        List<IEnemyData> Enemies { get; set; }
    }
}