using System;

namespace Models.Characters
{
    public class AttackEventArgs : EventArgs
    {
        public ICharacterModel EnemyAttacked { get; }

        public AttackEventArgs(ICharacterModel enemyAttacked)
        {
            EnemyAttacked = enemyAttacked;
        }
    }
}