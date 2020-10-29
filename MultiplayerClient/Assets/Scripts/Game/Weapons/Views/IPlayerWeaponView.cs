using System;
using Game.Views;

namespace Game.Weapons.Views
{
    public interface IPlayerWeaponView
    {
        event Action Clicked;
        ITextUiView DamageTextView { get; }
    }
}