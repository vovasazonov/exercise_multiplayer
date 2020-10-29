using System.Collections.Generic;
using Models.Characters;
using Models.Weapons;

namespace Models
{
    public class ModelManager : IModelManager
    {
        public ITrackableDictionary<int, IPlayerModel> PlayerModelDic { get; } = new TrackableDictionary<int, IPlayerModel>();
        public ITrackableDictionary<int, ICharacterModel> CharacterModelDic { get; } = new TrackableDictionary<int, ICharacterModel>();
        public IList<IWeaponModel> WeaponModelForAllPlayersList = new List<IWeaponModel>();

        public ModelManager()
        {
            InstantiateWeapons();
        }

        private void InstantiateWeapons()
        {
            var axeWeaponData = new WeaponData
            {
                Id = "axe",
                Damage = 2
            };
            
            var knifeWeaponData = new WeaponData
            {
                Id = "knife",
                Damage = 6
            };
            WeaponModelForAllPlayersList.Add(new WeaponModel(axeWeaponData));
            WeaponModelForAllPlayersList.Add(new WeaponModel(knifeWeaponData));
        }
    }
}