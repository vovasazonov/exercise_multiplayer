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

            InstantiateProperty("weapons", new Property(GetWeapons, GetDiffWeapons, SetWeapons, ContainsDiffWeapons, ResetDiffWeapons));
            InstantiateProperty("characters", new Property(GetCharacters, GetDiffCharacters, SetCharacters, ContainsDiffCharacters, ResetDiffCharacters));
            InstantiateProperty("players", new Property(GetPlayers, GetDiffPlayers, SetPlayers, ContainsDiffPlayers, ResetDiffPlayers));
        }

        private object GetDiffWeapons()
        {
            return _weaponsReplication.WriteDiff();
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

        private object GetDiffCharacters()
        {
           return _charactersReplication.WriteDiff();
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

        private object GetDiffPlayers()
        {
            return _playersReplication.WriteDiff();
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