using System;
using System.Collections.Generic;
using Models.Characters;
using Models.Weapons;

namespace Models
{
    public class WorldData : Replication, IWorldData
    {
        private readonly ExemplarsData<ICharacterData, CharacterData> _characterData = new ExemplarsData<ICharacterData, CharacterData>();
        private readonly ExemplarsData<IPlayerData, PlayerData> _playersData = new ExemplarsData<IPlayerData, PlayerData>();
        private readonly ExemplarsData<IWeaponData, WeaponData> _weaponsData = new ExemplarsData<IWeaponData, WeaponData>();
        public IExemplarsData<ICharacterData> CharacterData => _characterData;
        public IExemplarsData<IPlayerData> PlayersData => _playersData;
        public IExemplarsData<IWeaponData> WeaponsData => _weaponsData;

        public override void Read(Dictionary<string, object> data)
        {
            foreach (var dataId in data.Keys)
            {
                var value = data[dataId];

                switch (dataId)
                {
                    case nameof(CharacterData):
                        _characterData.Read((Dictionary<string, object>) value);
                        break;
                    case nameof(PlayersData):
                        _playersData.Read((Dictionary<string, object>) value);
                        break;
                    case nameof(WeaponsData):
                        _weaponsData.Read((Dictionary<string, object>) value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        protected override Dictionary<string, object> GetWhole()
        {
            return new Dictionary<string, object>
            {
                {nameof(CharacterData), _characterData.Write(ReplicationType.Whole)},
                {nameof(PlayersData), _playersData.Write(ReplicationType.Whole)},
                {nameof(WeaponsData), _weaponsData.Write(ReplicationType.Whole)}
            };
        }

        protected override Dictionary<string, object> GetDiff()
        {
            return new Dictionary<string, object>
            {
                {nameof(CharacterData), _characterData.Write(ReplicationType.Diff)},
                {nameof(PlayersData), _playersData.Write(ReplicationType.Diff)},
                {nameof(WeaponsData), _weaponsData.Write(ReplicationType.Diff)}
            };
        }
    }
}