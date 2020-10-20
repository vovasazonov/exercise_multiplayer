using System;
using Game.Views;

namespace Game.Weapons.Views
{
    public interface IWeaponButtonView
    {
        event Action Clicked;
        ITextUiView DamageTextView { get; }
    }
}