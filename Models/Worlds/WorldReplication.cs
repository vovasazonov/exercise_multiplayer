using Models.Characters;
using Models.Weapons;
using Replications;
using Serialization;

namespace Models
{
    public class WorldReplication : Replication
    {
        public WorldReplication(IWorldData worldData, ICustomCastObject castObject) : base(castObject)
        {
            InstantiateProperty("weapons", new ReplicationProperty(new WeaponsReplication(worldData.WeaponsData, castObject)));
            InstantiateProperty("characters", new ReplicationProperty(new CharactersReplication(worldData.CharacterData, castObject)));
            InstantiateProperty("players", new ReplicationProperty(new PlayersReplication(worldData.PlayersData, castObject)));
        }
    }
}