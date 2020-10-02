using UnityEngine;

namespace Descriptions
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Description/Weapon", order = 0)]
    public class DescriptionWeapon : ScriptableObject
    {
        public string Id;
        public uint Damage;
    }
}