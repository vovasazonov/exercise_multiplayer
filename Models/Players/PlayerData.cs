using System;
using System.Collections.Generic;

namespace Models
{
    public sealed class PlayerData : Replication, IPlayerData
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

        public override void Read(object data)
        {
            var dataDic = _customCastObject.To<Dictionary<string, object>>(data);
            foreach (var dataId in dataDic.Keys)
            {
                var value = dataDic[dataId];

                switch (dataId)
                {
                    case nameof(Score):
                        Score = _customCastObject.To<uint>(value);
                        break;
                    case nameof(ControllableCharacterExemplarId):
                        ControllableCharacterExemplarId = _customCastObject.To<int>(value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        protected override object GetWhole()
        {
            return new Dictionary<string, object>
            {
                {nameof(Score), Score},
                {nameof(ControllableCharacterExemplarId), ControllableCharacterExemplarId}
            };
        }

        private void OnScoreUpdated()
        {
            ScoreUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void OnControllableCharacterExemplarIdUpdated()
        {
            ControllableCharacterExemplarIdUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}