using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HealthPoints.Views
{
    public class TakeHealthPointButtonView : MonoBehaviour, ITakeHealthPointButtonView, IPointerClickHandler
    {
        public event Action Attack;

        private void OnAttack()
        {
            Attack?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnAttack();
        }
    }
}