using System.Collections.Generic;
using Game.Characters.Models;
using Game.Weapons.Models;

namespace Game
{
    public class ModelManager
    {
        public readonly ICharacterModel EnemyModel;
        public readonly List<IWeaponModel> WeaponModels = new List<IWeaponModel>();
        
        public ModelManager()
        {
            EnemyModel = new CharacterModel(3, 100);
            WeaponModels.Add(new WeaponModel(0,3));
            WeaponModels.Add(new WeaponModel(1,22));
        }
    }
}