using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Attacks.Views
{
    public class AttackButtonView : MonoBehaviour, IAttackButtonView, IPointerClickHandler
    {
        [SerializeField] private protected Text _damageText;
        public event Action Attack;

        public string Damage
        {
            set => _damageText.text = value;
        }

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