using System;
using Game.Characters.Views;
using Game.HealthPoints.Presenters;
using Models.Characters;

namespace Game.Characters.Presenters
{
    public class EnemyCharacterPresenter : IPresenter
    {
        private readonly ICharacterView _view;
        private readonly ICharacterModel _enemyCharacter;
        private readonly ICharacterModel _playerCharacter;
        private readonly HealthPointPresenter _healthPointPresenter;

        public EnemyCharacterPresenter(ICharacterView view, ICharacterModel enemyCharacter, ICharacterModel playerCharacter)
        {
            _view = view;
            _enemyCharacter = enemyCharacter;
            _playerCharacter = playerCharacter;

            _healthPointPresenter = new HealthPointPresenter(view.HealthPointTextView, _enemyCharacter.HealthPoint);
        }

        public void Activate()
        {
            _view.Clicked += OnClicked;
            _healthPointPresenter.Activate();
        }

        public void Deactivate()
        {
            _view.Clicked -= OnClicked;
            _healthPointPresenter.Deactivate();
        }

        private void OnClicked(object sender, EventArgs e)
        {
            AttackEnemy();
        }

        private void AttackEnemy()
        {
            _playerCharacter.Attack(_enemyCharacter);
        }
    }
}