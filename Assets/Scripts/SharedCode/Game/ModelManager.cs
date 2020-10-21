using System.Collections.Generic;
using Game.Characters.Models;
using Game.Weapons.Models;

namespace Game
{
    public class ModelManager
    {
        public readonly Dictionary<int, ICharacterModel> CharacterModelDic = new Dictionary<int, ICharacterModel>();
        public readonly Dictionary<int, IWeaponModel> WeaponModelDic = new Dictionary<int, IWeaponModel>();
        
        public ModelManager()
        {
            CharacterModelDic[0] = new CharacterModel("slime_enemy", 100);
            WeaponModelDic[0] = new WeaponModel("axe", 3);
            WeaponModelDic[1] = new WeaponModel("spell", 22);
        }
    }
}