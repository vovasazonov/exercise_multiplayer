using Replications;
using Serialization;

namespace Models
{
    public class PlayerReplication : Replication
    {
        public PlayerReplication(IPlayerData playerData, ICustomCastObject castObject) : base(castObject)
        {
            InstantiateProperty("score", new PrimitiveProperty<uint>(() => playerData.Score,obj => playerData.Score = _castObject.To<uint>(obj)));
            InstantiateProperty("controllable_character_id", new PrimitiveProperty<int>(() => playerData.ControllableCharacterExemplarId,obj => playerData.ControllableCharacterExemplarId = _castObject.To<int>(obj)));
        }
    }
}