using System;

namespace Models.Characters
{
    public class AttackEventArgs : EventArgs
    {
        private readonly ICharacterModel _enemyAttacked;

        public AttackEventArgs(ICharacterModel enemyAttacked)
        {
            _enemyAttacked = enemyAttacked;
        }
    }
}