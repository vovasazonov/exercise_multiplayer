using System.Collections.Generic;
using System.Linq;
using Game.Characters.Views;
using UnityEngine;

namespace Game.Characters
{
    public class CharacterViewPooler : MonoBehaviour, IViewPooler<ICharacterView>
    {
        [SerializeField] private protected CharacterView _characterViewPrefab;
        [SerializeField] private protected Transform _parent;
        private readonly HashSet<CharacterView> _characterFreeList = new HashSet<CharacterView>();
        private readonly HashSet<CharacterView> _characterBusyList = new HashSet<CharacterView>();

        public ICharacterView GetView()
        {
            var view = _characterFreeList.FirstOrDefault();

            if (view == null)
            {
                view = Instantiate(_characterViewPrefab, _parent);
            }
            else
            {
                _characterFreeList.Remove(view);
            }

            _characterBusyList.Add(view);
            view.gameObject.SetActive(true);

            return view;
        }

        public void ReturnView(ICharacterView character)
        {
            var unityObject = character as CharacterView;

            if (unityObject != null)
            {
                unityObject.gameObject.SetActive(false);
                _characterBusyList.Remove(unityObject);
                _characterFreeList.Add(unityObject);
            }
        }
    }
}