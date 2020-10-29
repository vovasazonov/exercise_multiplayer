using System.Collections.Generic;
using Models.Characters;
using Models.Weapons;

namespace Models
{
    public interface IModelManager
    {
        ITrackableDictionary<int,IPlayerModel> PlayerModelDic { get; }
        ITrackableDictionary<int,ICharacterModel> CharacterModelDic { get; }
        IList<IWeaponModel> WeaponModelForAllPlayersList { get; }
    }
}