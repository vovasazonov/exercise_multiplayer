using UnityEngine;
using UnityEngine.UI;

namespace HealthPoints.Views
{
    public class HealthPointTextView : MonoBehaviour, IHealthPointTextView
    {
        [SerializeField] private protected Text _pointsText;
        public int Points { set=> _pointsText.text = value.ToString(); }
    }
}