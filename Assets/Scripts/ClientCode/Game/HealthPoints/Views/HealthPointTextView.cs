using UnityEngine;
using UnityEngine.UI;

namespace Game.HealthPoints.Views
{
    public class HealthPointTextView : MonoBehaviour, IHealthPointTextView
    {
        [SerializeField] private protected Text _pointsText;
        
        public string Points { set=> _pointsText.text = value; }
    }
}