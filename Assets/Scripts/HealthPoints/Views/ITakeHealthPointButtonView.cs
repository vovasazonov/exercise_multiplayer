using System;

namespace HealthPoints.Views
{
    public interface ITakeHealthPointButtonView
    {
        event Action Attack;
    }
}