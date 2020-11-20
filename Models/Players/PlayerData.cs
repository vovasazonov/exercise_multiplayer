using System;
using System.Collections.Generic;

namespace Models
{
    public class PlayerData : Replication, IPlayerData
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
                _diff[nameof(Score)] = value;
                OnScoreUpdated();
            }
        }

        public int ControllableCharacterExemplarId
        {
            get => _controllableCharacterExemplarId;
            set
            {
                _controllableCharacterExemplarId = value;
                _diff[nameof(ControllableCharacterExemplarId)] = value;
                OnControllableCharacterExemplarIdUpdated();
            }
        }

        public override void Read(Dictionary<string, object> data)
        {
            foreach (var dataId in data.Keys)
            {
                var value = data[dataId];

                switch (dataId)
                {
                    case nameof(Score):
                        Score = (uint) value;
                        break;
                    case nameof(ControllableCharacterExemplarId):
                        ControllableCharacterExemplarId = (int) value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        protected override Dictionary<string, object> GetWhole()
        {
            return new Dictionary<string, object>
            {
                {nameof(Score), Score},
                {nameof(ControllableCharacterExemplarId), ControllableCharacterExemplarId}
            };
        }

        protected virtual void OnScoreUpdated()
        {
            ScoreUpdated?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnControllableCharacterExemplarIdUpdated()
        {
            ControllableCharacterExemplarIdUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}