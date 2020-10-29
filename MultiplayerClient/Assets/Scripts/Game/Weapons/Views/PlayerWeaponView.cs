using System;
using Game.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Weapons.Views
{
    public class PlayerWeaponView : MonoBehaviour, IPlayerWeaponView, IPointerClickHandler
    {
        public event Action Clicked;

        [SerializeField] private protected TextUiView _textUiView;
        
        public ITextUiView DamageTextView => _textUiView;

        private void OnClicked()
        {
            Clicked?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked();
        }
    }
}