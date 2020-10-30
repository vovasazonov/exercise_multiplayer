using System.Collections.Generic;
using Game.Characters;
using Game.Views;
using Game.Weapons.Views;
using UnityEngine;

namespace Game
{
    public class ViewManager : MonoBehaviour
    {
        public CharacterViewPooler CharacterViewPooler;
        public List<PlayerWeaponView> playerWeaponViewList;
    }
}