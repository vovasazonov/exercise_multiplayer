using System.Collections.Generic;
using Serialization;

namespace Models
{
    public class PlayersReplication : ExemplarsReplication<IPlayerData>
    {
        public PlayersReplication(ITrackableDictionary<int, IPlayerData> exemplarsData, ICustomCastObject castObject) : base(exemplarsData, castObject)
        {
        }

        protected override void InstantiateReplication(int exemplarId, IPlayerData data)
        {
            _exemplarsReplication.Add(exemplarId, new PlayerReplication(data, _castObject));
        }

        protected override void InstantiateData(int exemplarId)
        {
            _exemplarsData.Add(exemplarId, new PlayerData());
        }
    }
}