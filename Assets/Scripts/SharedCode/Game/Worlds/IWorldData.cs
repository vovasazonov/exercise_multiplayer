using System;

namespace Game.Worlds
{
    public interface IWorldData
    {
        event Action DataChanged;
    }
}