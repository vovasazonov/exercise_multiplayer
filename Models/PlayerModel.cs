using System;
using System.Collections.Generic;
using Models.Characters;

namespace Models
{
    public class PlayerModel : IPlayerModel
    {
        private readonly ITrackableDictionary<int, ICharacterModel> _characterModelDic;
        public event EventHandler ScoreChanged;

        private int? _controllableCharacterExemplarId;

        public int Score { get; private set; }

        public int ControllableCharacterExemplarId
        {
            get => (int) _controllableCharacterExemplarId;
            set
            {
                if (_controllableCharacterExemplarId != null)
                {
                    RemoveCharacterListeners();
                }

                _controllableCharacterExemplarId = value;
                AddCharacterListeners();
            }
        }

        public PlayerModel(ITrackableDictionary<int, ICharacterModel> characterModelDic)
        {
            _characterModelDic = characterModelDic;
        }

        void AddCharacterListeners()
        {
            _characterModelDic[ControllableCharacterExemplarId].EnemyAttacked += OnEnemyAttacked;
        }

        void RemoveCharacterListeners()
        {
            _characterModelDic[ControllableCharacterExemplarId].EnemyAttacked -= OnEnemyAttacked;
        }

        private void OnEnemyAttacked(object sender, AttackEventArgs e)
        {
            Score += (int) _characterModelDic[ControllableCharacterExemplarId].HoldWeapon.Damage;
            OnScoreChanged();
        }

        private void OnScoreChanged()
        {
            ScoreChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}