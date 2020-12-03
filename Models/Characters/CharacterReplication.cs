using Replications;
using Serialization;

namespace Models.Characters
{
    public class CharacterReplication : Replication
    {
        public CharacterReplication(ICharacterData characterData, ICustomCastObject castObject) : base(castObject)
        {
            var healthReplication = new HealthPointReplication(characterData.HealthPointData, castObject);
            InstantiateProperty("health", new ReplicationProperty(healthReplication));
        }
    }
}