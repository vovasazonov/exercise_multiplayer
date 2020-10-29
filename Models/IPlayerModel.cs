using System;

namespace Models
{
    public interface IPlayerModel
    {
        event EventHandler ScoreChanged;
        int Score { get; }
        int ControllableCharacterExemplarId { get; set; }
    }
}