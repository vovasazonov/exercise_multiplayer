using System;
using Game.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Characters.Views
{
    public class CharacterView : MonoBehaviour, ICharacterView
    {
        public event EventHandler Clicked;
        [SerializeField] private protected Button _button;
        [SerializeField] private protected TextUiView _healthPointTextView;

        private void Awake()
        {
            AddButtonListener();
        }

        private void OnDestroy()
        {
            RemoveButtonListener();
        }

        private void AddButtonListener()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void RemoveButtonListener()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        public ITextUiView HealthPointTextView => _healthPointTextView;

        private void OnClicked()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }
}