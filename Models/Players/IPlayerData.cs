using System;

namespace Models
{
    public interface IPlayerData
    {
        event EventHandler ScoreUpdated;
        event EventHandler ControllableCharacterExemplarIdUpdated;
        
        uint Score { get; set; }
        int ControllableCharacterExemplarId { get; set; }
    }
}