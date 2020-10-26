using System;
using Models.Weapons;

namespace Models.Characters
{
    public interface ICharacterModel
    {
        event EventHandler<AttackEventArgs> EnemyAttacked; 
        string Id { get; }
        IHealthPointModel HealthPoint { get; }
        IWeaponModel HoldWeapon { get; }
        
        void Attack(ICharacterModel enemy);

        void ChangeHoldWeapon(IWeaponModel weapon);
    }
}