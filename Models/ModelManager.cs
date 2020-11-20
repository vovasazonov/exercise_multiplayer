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
            CharactersModel = new CharactersModel(worldData.CharacterData);
            PlayersModel = new PlayersModel(worldData.PlayersData,CharactersModel);            
            WeaponsModel = new WeaponsModel(worldData.WeaponsData);
        }
    }
}