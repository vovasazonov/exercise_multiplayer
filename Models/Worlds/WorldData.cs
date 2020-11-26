using System.Collections.Generic;
using Models.Characters;
using Models.Weapons;

namespace Models
{
    public sealed class WorldData : IWorldData
    {
        public ITrackableDictionary<int,ICharacterData> CharacterData { get; } = new TrackableDictionary<int,ICharacterData>();
        public ITrackableDictionary<int,IPlayerData> PlayersData { get; } = new TrackableDictionary<int,IPlayerData>();
        public ITrackableDictionary<int,IWeaponData> WeaponsData { get; } = new TrackableDictionary<int,IWeaponData>();
    }
}