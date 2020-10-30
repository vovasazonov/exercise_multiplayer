using System.Collections.Generic;
using Models.Characters;
using Models.Weapons;

namespace Models
{
    public class ModelManager : IModelManager
    {
        public ITrackableDictionary<int, IPlayerModel> PlayerModelDic { get; } = new TrackableDictionary<int, IPlayerModel>();
        public ITrackableDictionary<int, ICharacterModel> CharacterModelDic { get; } = new TrackableDictionary<int, ICharacterModel>();
        public IDictionary<string, IWeaponModel> GameWeaponModelDic { get; } = new Dictionary<string, IWeaponModel>();

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
            GameWeaponModelDic.Add(axeWeaponData.Id, new WeaponModel(axeWeaponData));
            GameWeaponModelDic.Add(knifeWeaponData.Id, new WeaponModel(knifeWeaponData));
        }
    }
}