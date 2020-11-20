using System;

namespace Models
{
    public interface IPlayerModel
    {
        event EventHandler ScoreChanged;
        event EventHandler ControllableCharacterExemplarIdChanged;
        
        uint Score { get; }
        int ControllableCharacterExemplarId { get; }
    }
}