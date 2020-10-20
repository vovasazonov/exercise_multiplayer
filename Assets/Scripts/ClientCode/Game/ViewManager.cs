using System.Collections.Generic;
using Game.Views;
using Game.Weapons.Views;
using UnityEngine;

namespace Game
{
    public class ViewManager : MonoBehaviour
    {
        public TextUiView EnemyHealthText;
        public List<WeaponButtonView> WeaponButtonList;
    }
}