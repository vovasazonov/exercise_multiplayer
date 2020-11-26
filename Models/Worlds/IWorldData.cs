using System;
using System.Collections.Generic;
using Models.Characters;
using Models.Weapons;

namespace Models
{
    public interface IWorldData
    {
        ITrackableDictionary<int, ICharacterData> CharacterData { get; }
        ITrackableDictionary<int, IPlayerData> PlayersData { get; }
        ITrackableDictionary<int, IWeaponData> WeaponsData { get; }
    }
}