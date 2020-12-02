using Replications;
using Serialization;

namespace Models
{
    public class PlayerReplication : Replication
    {
        private readonly IPlayerData _playerData;
        private uint _oldScore;
        private int _oldControllableCharacterExemplarId;

        public PlayerReplication(IPlayerData playerData, ICustomCastObject castObject) : base(castObject)
        {
            _playerData = playerData;

            InstantiateProperty("score", new Property(GetScore, GetScore, SetScore, ContainsDiffScore, ResetDiffScore));
            InstantiateProperty("control_character_id", new Property(GetCharacterId, GetScore, SetCharacterId, ContainsDiffCharacterId, ResetDiffCharacterId));
        }

        private object GetScore()
        {
            return _playerData.Score;
        }

        private void SetScore(object obj)
        {
            _playerData.Score = _castObject.To<uint>(obj);
        }

        private bool ContainsDiffScore()
        {
            return _oldScore != _playerData.Score;
        }

        private void ResetDiffScore()
        {
            _oldScore = _playerData.Score;
        }

        private object GetCharacterId()
        {
            return _playerData.ControllableCharacterExemplarId;
        }

        private void SetCharacterId(object obj)
        {
            _playerData.ControllableCharacterExemplarId = _castObject.To<int>(obj);
        }

        private bool ContainsDiffCharacterId()
        {
            return _oldControllableCharacterExemplarId != _playerData.ControllableCharacterExemplarId;
        }

        private void ResetDiffCharacterId()
        {
            _oldControllableCharacterExemplarId = _playerData.ControllableCharacterExemplarId;
        }
    }
}