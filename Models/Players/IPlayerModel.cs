using System;

namespace Models
{
    public interface IPlayerModel
    {
        event Action ScoreChanged;
        event Action ControllableCharacterExemplarIdChanged;
        
        uint Score { get; }
        int ControllableCharacterExemplarId { get; }
    }
}