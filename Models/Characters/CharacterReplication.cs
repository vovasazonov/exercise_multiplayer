using Replications;
using Serialization;

namespace Models.Characters
{
    public class CharacterReplication : Replication
    {
        private readonly ICharacterData _characterData;
        private readonly HealthPointReplication _healthPointReplication;

        public CharacterReplication(ICharacterData characterData, ICustomCastObject castObject) : base(castObject)
        {
            _characterData = characterData;
            _healthPointReplication = new HealthPointReplication(_characterData.HealthPointData, castObject);

            InstantiateProperty("health", new Property(GetHealth, GetDiffHealth, SetHealth, IsHealthChanged, ResetDiffHealth));
        }

        private object GetDiffHealth()
        {
            return _healthPointReplication.WriteDiff();
        }

        private object GetHealth()
        {
            return _healthPointReplication.WriteWhole();
        }

        private void SetHealth(object obj)
        {
            _healthPointReplication.Read(obj);
        }

        private bool IsHealthChanged()
        {
            return _healthPointReplication.ContainsDiff();
        }

        private void ResetDiffHealth()
        {
            _healthPointReplication.ResetDiff();
        }
    }
}