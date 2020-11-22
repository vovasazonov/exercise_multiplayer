using System;
using Models.Characters;
using Models.Weapons;

namespace Models
{
    public interface IWorldData
    {
        event EventHandler Updated;
        IExemplarsData<ICharacterData> CharacterData { get; }
        IExemplarsData<IPlayerData> PlayersData { get; }
        IExemplarsData<IWeaponData> WeaponsData { get; }
    }
}