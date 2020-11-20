using System;

namespace Models.Weapons
{
    public interface IWeaponData
    {
        event EventHandler DamageUpdated;
        string Id { get; set; }
        uint Damage { get; set; }
    }
}