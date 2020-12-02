using System;
using System.Collections.Generic;

namespace Models
{
    public sealed class PlayerData : IPlayerData
    {
        public event EventHandler ScoreUpdated;
        public event EventHandler ControllableCharacterExemplarIdUpdated;

        private uint _score;
        private int _controllableCharacterExemplarId;

        public uint Score
        {
            get => _score;
            set
            {
                _score = value;
                CallScoreUpdated();
            }
        }

        public int ControllableCharacterExemplarId
        {
            get => _controllableCharacterExemplarId;
            set
            {
                _controllableCharacterExemplarId = value;
                CallControllableCharacterExemplarIdUpdated();
            }
        }

        private void CallScoreUpdated()
        {
            ScoreUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void CallControllableCharacterExemplarIdUpdated()
        {
            ControllableCharacterExemplarIdUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}