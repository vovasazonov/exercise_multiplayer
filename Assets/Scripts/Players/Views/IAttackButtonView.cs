using System;

namespace Players.Views
{
    public interface IAttackButtonView
    {
        event Action Attack;
    }
}