using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class TextUiView : MonoBehaviour, ITextUiView
    {
        [SerializeField] private protected Text _textUi;
        
        public string TextUi { set=> _textUi.text = value; }
    }
}