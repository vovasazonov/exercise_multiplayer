using System.Collections.Generic;
using Models.Characters;

namespace Models
{
    public interface IModelManager
    {
        ITrackableDictionary<int,ICharacterModel> CharacterModelDic { get; }
    }
}