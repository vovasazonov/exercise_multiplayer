using System;

namespace Models.Characters
{
    public interface ICharacterData
    {
        event Action HoldWeaponUpdated;
        
        string Id { get; set; }
        IHealthPointData HealthPointData { get; }
        int HoldWeaponExemplarId { get; set; }
    }
}