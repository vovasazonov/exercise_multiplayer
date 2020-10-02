using System;

namespace Attacks.Views
{
    public interface IAttackButtonView
    {
        event Action Attack;
        string Damage { set; }
    }
}