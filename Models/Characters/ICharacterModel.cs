using Models.Weapons;

namespace Models.Characters
{
    public delegate void EnemyAttackedHandler(ICharacterModel enemyAttacked);

    public delegate void HoldWeaponChangedHandler(int weaponExemplarId);

    public interface ICharacterModel
    {
        event EnemyAttackedHandler EnemyAttacked; 
        event HoldWeaponChangedHandler HoldWeaponChanged; 

        string Id { get; }
        IHealthPointModel HealthPoint { get; }
        IWeaponModel HoldWeapon { get; }
        
        void Attack(ICharacterModel enemy);

        void ChangeHoldWeapon(int weaponExemplarId);
    }
}