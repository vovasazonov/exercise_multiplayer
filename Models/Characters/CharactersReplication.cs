using System.Collections.Generic;
using Serialization;

namespace Models.Characters
{
    public class CharactersReplication : ExemplarsReplication<ICharacterData>
    {
        public CharactersReplication(ITrackableDictionary<int, ICharacterData> exemplarsData, ICustomCastObject castObject) : base(exemplarsData, castObject)
        {
        }

        protected override void InstantiateReplication(int exemplarId, ICharacterData data)
        {
            _exemplarsReplication.Add(exemplarId, new CharacterReplication(data, _castObject));
        }

        protected override void InstantiateData(int exemplarId)
        {
            _exemplarsData.Add(exemplarId, new CharacterData());
        }
    }
}