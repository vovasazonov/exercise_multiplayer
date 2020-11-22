using Models.Characters;
using Models.Weapons;

namespace Models
{
    public class ModelManager : IModelManager
    {
        public IExemplarsModel<IPlayerModel> PlayersModel { get; } 
        public IExemplarsModel<ICharacterModel> CharactersModel { get; }
        public IExemplarsModel<IWeaponModel> WeaponsModel { get; }
        
        public ModelManager(IWorldData worldData)
        {
            WeaponsModel = new WeaponsModel(worldData.WeaponsData);
            CharactersModel = new CharactersModel(worldData.CharacterData, WeaponsModel);
            PlayersModel = new PlayersModel(worldData.PlayersData,CharactersModel);            
        }
    }
}