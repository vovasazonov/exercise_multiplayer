using System;

namespace Models
{
    public interface IPlayerData
    {
        event Action ScoreUpdated;
        event Action ControllableCharacterExemplarIdUpdated;
        
        uint Score { get; set; }
        int ControllableCharacterExemplarId { get; set; }
    }
}