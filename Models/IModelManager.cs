using Models.Characters;
using Models.Weapons;

namespace Models
{
    public interface IModelManager
    {
        IExemplarsModel<IPlayerModel> PlayersModel { get; }
        IExemplarsModel<ICharacterModel> CharactersModel { get; }
        IExemplarsModel<IWeaponModel> WeaponsModel { get; }
    }
}