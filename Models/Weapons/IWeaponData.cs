using System;

namespace Models.Weapons
{
    public interface IWeaponData
    {
        event Action DamageUpdated;
        string Id { get; set; }
        uint Damage { get; set; }
    }
}