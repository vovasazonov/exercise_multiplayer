using System;
using Game.Views;

namespace Game.Characters.Views
{
    public interface ICharacterView
    {
        event EventHandler Clicked;
        ITextUiView HealthPointTextView { get; }
    }
}