using System;
using System.Collections.Generic;
using Models.Characters;

namespace Models
{
    public class PlayerModel : IPlayerModel
    {
        public event EventHandler ScoreChanged;
        public event EventHandler ControllableCharacterExemplarIdChanged;

        private readonly IPlayerData _data;
        private readonly ITrackableDictionary<int, ICharacterModel> _characterModelDic;
        private int? _oldControllableCharacterExemplarId;

        public uint Score { get; private set; }
        public int ControllableCharacterExemplarId => _data.ControllableCharacterExemplarId;

        public PlayerModel(IPlayerData data, ITrackableDictionary<int, ICharacterModel> characterModelDic)
        {
            _data = data;
            _characterModelDic = characterModelDic;

            _data.ScoreUpdated += OnScoreChanged;
            _data.ControllableCharacterExemplarIdUpdated += OnControllableCharacterExemplarIdUpdated;
        }

        void AddCharacterListeners(int id)
        {
            _characterModelDic[id].EnemyAttacked += OnEnemyAttacked;
        }

        void RemoveCharacterListeners(int id)
        {
            _characterModelDic[id].EnemyAttacked -= OnEnemyAttacked;
        }

        private void OnEnemyAttacked(object sender, AttackEventArgs e)
        {
            Score += _characterModelDic[ControllableCharacterExemplarId].HoldWeapon.Damage;
        }

        private void OnScoreChanged(object sender, EventArgs e)
        {
            OnScoreChanged();
        }

        private void OnScoreChanged()
        {
            ScoreChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnControllableCharacterExemplarIdUpdated(object sender, EventArgs e)
        {
            if (_oldControllableCharacterExemplarId != null)
            {
                RemoveCharacterListeners((int)_oldControllableCharacterExemplarId);
            }

            _oldControllableCharacterExemplarId = ControllableCharacterExemplarId;
            AddCharacterListeners(ControllableCharacterExemplarId);
            
            OnControllableCharacterExemplarIdChanged();
        }

        private void OnControllableCharacterExemplarIdChanged()
        {
            ControllableCharacterExemplarIdChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}