using System;

namespace Models.Characters
{
    public interface ICharacterData
    {
        event EventHandler HoldWeaponUpdated;
        
        string Id { get; set; }
        IHealthPointData HealthPointData { get; }
        int HoldWeaponExemplarId { get; set; }
    }
}