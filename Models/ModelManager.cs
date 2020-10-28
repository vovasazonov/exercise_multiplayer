using System.Collections.Generic;
using Models.Characters;

namespace Models
{
    public class ModelManager : IModelManager
    {
        public ITrackableDictionary<int, ICharacterModel> CharacterModelDic { get; } = new TrackableDictionary<int, ICharacterModel>();
    }
}