using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Players.Views
{
    public class AttackButtonView : MonoBehaviour, IAttackButtonView, IPointerClickHandler
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