using System;
using Replications;
using Serialization;

namespace Models
{
    public class PlayerReplication : Replication
    {
        private readonly IPlayerData _playerData;

        public PlayerReplication(IPlayerData playerData, ICustomCastObject castObject) : base(castObject)
        {
            _playerData = playerData;

            _playerData.ScoreUpdated += OnScoreUpdated;
            _playerData.ControllableCharacterExemplarIdUpdated += OnControllableCharacterIdUpdated;

            _getterDic.Add(nameof(_playerData.Score), () => _playerData.Score);
            _setterDic.Add(nameof(_playerData.Score), obj => _playerData.Score = castObject.To<uint>(obj));
            _getterDic.Add(nameof(_playerData.ControllableCharacterExemplarId), () => _playerData.ControllableCharacterExemplarId);
            _setterDic.Add(nameof(_playerData.ControllableCharacterExemplarId), obj => _playerData.ControllableCharacterExemplarId = castObject.To<int>(obj));
        }

        private void OnScoreUpdated(object sender, EventArgs e) => _diffDic[nameof(_playerData.Score)] = _playerData.Score;
        private void OnControllableCharacterIdUpdated(object sender, EventArgs e) => _diffDic[nameof(_playerData.ControllableCharacterExemplarId)] = _playerData.ControllableCharacterExemplarId;
    }
}