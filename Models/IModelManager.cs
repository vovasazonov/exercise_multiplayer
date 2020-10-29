using System.Collections.Generic;
using Models.Characters;

namespace Models
{
    public interface IModelManager
    {
        ITrackableDictionary<int,IPlayerModel> PlayerModelDic { get; }
        ITrackableDictionary<int,ICharacterModel> CharacterModelDic { get; }
    }
}