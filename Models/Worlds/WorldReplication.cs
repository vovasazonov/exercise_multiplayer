using Models.Characters;
using Models.Weapons;
using Replications;
using Serialization;

namespace Models
{
    public class WorldReplication : Replication
    {
        private readonly IWorldData _worldData;
        private readonly WeaponsReplication _weaponsReplication;
        private readonly CharactersReplication _charactersReplication;
        private readonly PlayersReplication _playersReplication;

        public WorldReplication(IWorldData worldData, ICustomCastObject castObject) : base(castObject)
        {
            _worldData = worldData;

            _weaponsReplication = new WeaponsReplication(worldData.WeaponsData, castObject);
            _charactersReplication = new CharactersReplication(worldData.CharacterData, castObject);
            _playersReplication = new PlayersReplication(worldData.PlayersData, castObject);

            _getterDic.Add(nameof(_worldData.WeaponsData), () => _weaponsReplication.WriteWhole());
            _setterDic.Add(nameof(_worldData.WeaponsData), obj => _weaponsReplication.Read(obj));
            _getterDic.Add(nameof(_worldData.CharacterData), () => _charactersReplication.WriteWhole());
            _setterDic.Add(nameof(_worldData.CharacterData), obj => _charactersReplication.Read(obj));
            _getterDic.Add(nameof(_worldData.PlayersData), () => _playersReplication.WriteWhole());
            _setterDic.Add(nameof(_worldData.PlayersData), obj => _playersReplication.Read(obj));
        }

        public override object WriteDiff()
        {
            _diffDic[nameof(_worldData.WeaponsData)] = _weaponsReplication.WriteDiff();
            _diffDic[nameof(_worldData.CharacterData)] = _charactersReplication.WriteDiff();
            _diffDic[nameof(_worldData.PlayersData)] = _playersReplication.WriteDiff();
            
            return base.WriteDiff();
        }
    }
}