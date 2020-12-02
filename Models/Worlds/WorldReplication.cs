using Models.Characters;
using Models.Weapons;
using Replications;
using Serialization;

namespace Models
{
    public class WorldReplication : Replication
    {
        private readonly WeaponsReplication _weaponsReplication;
        private readonly CharactersReplication _charactersReplication;
        private readonly PlayersReplication _playersReplication;

        public WorldReplication(IWorldData worldData, ICustomCastObject castObject) : base(castObject)
        {
            _weaponsReplication = new WeaponsReplication(worldData.WeaponsData, castObject);
            _charactersReplication = new CharactersReplication(worldData.CharacterData, castObject);
            _playersReplication = new PlayersReplication(worldData.PlayersData, castObject);

            InstantiateProperty("weapons", new Property(GetWeapons, SetWeapons, ContainsDiffWeapons, ResetDiffWeapons));
            InstantiateProperty("characters", new Property(GetCharacters, SetCharacters, ContainsDiffCharacters, ResetDiffCharacters));
            InstantiateProperty("players", new Property(GetPlayers, SetPlayers, ContainsDiffPlayers, ResetDiffPlayers));
        }

        private object GetWeapons()
        {
            return _weaponsReplication.WriteWhole();
        }

        private void SetWeapons(object obj)
        {
            _weaponsReplication.Read(obj);
        }

        private bool ContainsDiffWeapons()
        {
            return _weaponsReplication.ContainsDiff();
        }

        private void ResetDiffWeapons()
        {
            _weaponsReplication.ResetDiff();
        }

        private object GetCharacters()
        {
            return _charactersReplication.WriteWhole();
        }

        private void SetCharacters(object obj)
        {
            _charactersReplication.Read(obj);
        }

        private bool ContainsDiffCharacters()
        {
            return _charactersReplication.ContainsDiff();

        }

        private void ResetDiffCharacters()
        {
            _charactersReplication.ResetDiff();
        }

        private object GetPlayers()
        {
            return _playersReplication.WriteWhole();
        }

        private void SetPlayers(object obj)
        {
            _playersReplication.Read(obj);
        }

        private bool ContainsDiffPlayers()
        {
            return _playersReplication.ContainsDiff();
        }

        private void ResetDiffPlayers()
        {
            _playersReplication.ResetDiff();
        }
    }
}