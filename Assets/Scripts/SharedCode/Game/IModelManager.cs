using System.Collections.Generic;
using Game.Characters.Models;
using Game.Weapons.Models;

namespace Game
{
    public interface IModelManager
    {
        IDictionary<int, ICharacterModel> CharacterModelDic { get; }
        IDictionary<int, IWeaponModel> WeaponModelDic { get; }
    }
}