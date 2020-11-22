using System;
using System.Collections.Generic;
using Models.Characters;
using Models.Weapons;

namespace Models
{
    public sealed class WorldData : Replication, IWorldData
    {
        public event EventHandler Updated;
        
        private readonly ExemplarsData<ICharacterData, CharacterData> _charactersData;
        private readonly ExemplarsData<IPlayerData, PlayerData> _playersData;
        private readonly ExemplarsData<IWeaponData, WeaponData> _weaponsData;
        public IExemplarsData<ICharacterData> CharacterData => _charactersData;
        public IExemplarsData<IPlayerData> PlayersData => _playersData;
        public IExemplarsData<IWeaponData> WeaponsData => _weaponsData;

        public WorldData()
        {
            _charactersData = new ExemplarsData<ICharacterData, CharacterData>();
            _playersData = new ExemplarsData<IPlayerData, PlayerData>();
            _weaponsData = new ExemplarsData<IWeaponData, WeaponData>();
            
            _charactersData.Updated += OnUpdated;
            _playersData.Updated += OnUpdated;
            _weaponsData.Updated += OnUpdated;
        }

        public override void SetCustomCast(ICustomCastObject customCastObject)
        {
            base.SetCustomCast(customCastObject);
            
            _charactersData.SetCustomCast(customCastObject);
            _playersData.SetCustomCast(customCastObject);
            _weaponsData.SetCustomCast(customCastObject);
        }

        public override void Read(object data)
        {
            var dataDic = _customCastObject.To<Dictionary<string, object>>(data);
            foreach (var dataId in dataDic.Keys)
            {
                var value = dataDic[dataId];

                switch (dataId)
                {
                    case nameof(WeaponsData):
                        _weaponsData.Read(_customCastObject.To<Dictionary<string, object>>(value));
                        break;
                    case nameof(CharacterData):
                        _charactersData.Read(_customCastObject.To<Dictionary<string, object>>(value));
                        break;
                    case nameof(PlayersData):
                        _playersData.Read(_customCastObject.To<Dictionary<string, object>>(value));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        protected override object GetWhole()
        {
            return new Dictionary<string, object>
            {
                {nameof(WeaponsData), _weaponsData.Write(ReplicationType.Whole)},
                {nameof(CharacterData), _charactersData.Write(ReplicationType.Whole)},
                {nameof(PlayersData), _playersData.Write(ReplicationType.Whole)}
            };
        }

        protected override object GetDiff()
        {
            return new Dictionary<string, object>
            {
                {nameof(WeaponsData), _weaponsData.Write(ReplicationType.Diff)},
                {nameof(CharacterData), _charactersData.Write(ReplicationType.Diff)},
                {nameof(PlayersData), _playersData.Write(ReplicationType.Diff)}
            };
        }

        private void OnUpdated(object sender, EventArgs e)
        {
            OnUpdated();
        }

        private void OnUpdated()
        {
            Updated?.Invoke(this, EventArgs.Empty);
        }
    }
}