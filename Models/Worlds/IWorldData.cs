using Models.Characters;
using Models.Weapons;

namespace Models
{
    public interface IWorldData
    {
        IExemplarsData<ICharacterData> CharacterData { get; }
        IExemplarsData<IPlayerData> PlayersData { get; }
        IExemplarsData<IWeaponData> WeaponsData { get; }
    }
}